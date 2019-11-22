#define FastCopyES
using System;
using System.Collections.Generic;
using Kernel.Combiners;
using Kernel.Arithmetic;
using static CarFamily;
using Kernel.Utilities;
using System.Linq;
using static Kernel.Primitives.Primitives;
using Kernel.BaseTypes;
using Object = Kernel.BaseTypes.Object;
using Environment = Kernel.BaseTypes.Environment;
using Boolean = Kernel.BaseTypes.Boolean;
using String = Kernel.BaseTypes.String;
using Kernel.Primitives.DynamicBinding.Attributes;

namespace Kernel.Primitives
{
    public static class Applicatives
    {
        [Primitive("for-each", 1, true)]
        [TypeAssertion(0, typeof(Applicative))]
        [VariadicTypeAssertion(typeof(List), 1)]
        public static Inert ForEach(Applicative app, List lists)
        {
            if (!lists.Any<Object>()) throw new ArgumentException("Lists list must not be empty");
            Integer starterLength = ListMetrics.Pairs(GetListMetrics(lists[0]));

            if ((lists[0] as Pair).ContainsCycle && lists.Any<List>(list => !list.ContainsCycle))
                throw new ArgumentException("A list is cyclic, so they must all be");

            if (lists.Any<Object>(l => ListMetrics.Pairs(GetListMetrics(l)) != starterLength))
                throw new ArgumentException("A list does not have the same length as the others");

            HashSet<List> visitedTuples = new HashSet<List>();

            while (lists[0] != null && visitedTuples.Add(lists))
            {
                app.Invoke(lists.Select<Pair>(Car<Object>));
                lists = lists.Select<Pair>(Cdr<Pair>);
            }

            return Inert.Instance;
        }

        [Primitive("display", 2)]
        [TypeAssertion(1, typeof(Port), optional: true)]
        [PredicateAssertion(1, typeof(Primitives), "IsOutputPort", optional: true)]
        public static Inert Display(Object obj, Port p)
        {
            p ??= Port.StandardOutput;
            p.Writer.Write(obj);
            p.Writer.Flush();
            return Inert.Instance;
        }

        [Primitive("read")]
        public static Object Read()
        {
            static bool Valid(string representation)
            {
                int braces = 0;
                bool inString = false;
                for (int i = 0; i < representation.Count(); i++)
                {
                    switch (representation[i])
                    {
                        case '(':
                            if (!inString) braces++;
                            break;
                        case ')':
                            if (!inString) braces--;
                            break;
                        case '"':
                            inString = !inString;
                            break;
                        case '\\':
                            i++;
                            break;
                    }
                }
                return braces == 0 && !inString && representation.Trim().Any();
            }

            string input = string.Empty;

            do
            {
                input += Console.ReadLine();
            }
            while (!Valid(input));

            return Parser.Parser.Parse(input.Trim());
        }

        #region Pair

        [Primitive("cons", 2)]
        public static Object Cons(Object car, Object cdr) => new Pair(car, cdr);

        [Primitive("set-car!", 2)]
        [TypeAssertion(0, typeof(Pair))]
        [MutabilityAssertion(0)]
        public static Inert SetCar(Pair p, Object obj)
        {
            p.Car = obj;
            return Inert.Instance;
        }


        [Primitive("set-cdr!", 2)]
        [TypeAssertion(0, typeof(Pair))]
        [MutabilityAssertion(0)]
        public static Inert SetCdr(Pair p, Object obj)
        {
            p.Cdr = obj;
            return Inert.Instance;
        }

        #endregion

        [Primitive("copy-es-immutable", 1)]
        public static Object CopyEvaluationStructureImmutable(Object obj)
        {
            if (!(obj is Pair p)) return obj;

            // HACK Why bother with creating a new one?
#if FastCopyES
            if (!p.Mutable) return p;
#endif
            return new Pair(CopyEvaluationStructureImmutable(p.Car),
                            CopyEvaluationStructureImmutable(p.Cdr), false);
        }


        [Primitive("eval", 2)]
        [TypeAssertion(1, typeof(Environment))]
        public static Object Eval(Object tailContext, Environment env) => Evaluate(tailContext, env);

        [Primitive("wrap", 1)]
        [TypeAssertion(0, typeof(Combiner))]
        public static Applicative Wrap(Combiner c) => new Applicative(c);

        [Primitive("unwrap", 1)]
        [TypeAssertion(0, typeof(Applicative))]
        public static Combiner Unwrap(Applicative app) => app.Combiner;

        [Primitive("list", 0, true)]
        public static Object List(List objects) => objects;

        [Primitive("list*", 0, true)]
        [PredicateAssertion(0, typeof(Primitives), nameof(ContainsCycle), true)]
        public static Object ListStar(List objects)
        {
            if (objects is Null || objects.ContainsCycle) throw new ArgumentException("List* requires a nonempty acyclic list.");
            if (objects.Count() == 1) return objects[0];
            Pair head = new Pair(objects[0]);
            Pair tail = head;
            Object lastElement = objects.Skip(1).ForEachReturnLast<Object>((obj) => tail = tail.Append(obj));
            tail.Cdr = lastElement;
            return head;
        }

        [Primitive("apply", 3)]
        [TypeAssertion(0, typeof(Applicative))]
        [TypeAssertion(2, typeof(Environment), optional: true)]
        public static Object Apply(Applicative a, Object p, Environment env)
        {
            env ??= Environment.Current;
            List list = p is List l ? l : new Pair(p);
            return Evaluate(a.Combiner, list, env);
        }

        static readonly Dictionary<Object, Pair> immutableMetricsCache = new Dictionary<Object, Pair>();
        [Primitive("get-list-metrics", 1)]
        public static Pair GetListMetrics(Object obj)
        {
            if (!obj.Mutable && immutableMetricsCache.ContainsKey(obj))
                return immutableMetricsCache[obj];
            long pairs = 0;
            long nilCount = 0;
            long acyclicLength = 0;
            long cycleLength = 0;
            if (obj is Pair p)
            {
                HashSet<Pair> visitedPairs = new HashSet<Pair>();
                while (visitedPairs.Add(p))
                {
                    if (p.Cdr is Pair pNew)
                        p = pNew;
                    else
                    {
                        if (p.Cdr is Null) nilCount++;
                        break;
                    }
                }
                while (visitedPairs.Remove(p))
                {
                    cycleLength++;
                    p = p.Cdr as Pair;
                }
                acyclicLength = visitedPairs.Count;
                pairs = acyclicLength + cycleLength;
            }
            Pair result = new Pair(
                Integer.Get(pairs),
                Integer.Get(nilCount),
                Integer.Get(acyclicLength),
                Integer.Get(cycleLength)
            );
            if (!obj.Mutable)
                immutableMetricsCache.Add(obj, result);
            return result;
        }

        [Primitive("list-tail", 2)]
        [TypeAssertion(1, typeof(Integer))]
        [NonNegativityAssertion(1)]
        public static Object ListTail(Object obj, Integer i)
        {
            Object result = obj;
            for (int depth = 0; depth < i; depth++)
            {
                if (!(result is Pair p))
                    throw new ArgumentException("First argument is not a deep enough list");
                result = p.Cdr;
            }
            return result;
        }

        [Primitive("encycle!", 3)]
        [TypeAssertion(1, typeof(Integer))]
        [NonNegativityAssertion(1)]
        [TypeAssertion(2, typeof(Integer))]
        [NonNegativityAssertion(2)]
        public static Object Encycle(Object obj, Integer i1, Integer i2)
        {
            if (i2 == 0) return Inert.Instance;

            Object start = ListTail(obj, (i1 + i2 - 1) as Integer);

            if (!(start is Pair p))
                throw new ArgumentException("List is not deep enough to encycle");

            p.Cdr = ListTail(obj, i1);

            return Inert.Instance;
        }


        [Primitive("map", 1, true)]
        [TypeAssertion(0, typeof(Applicative))]
        [VariadicTypeAssertion(typeof(Pair), 1)]
        public static Object Map(Applicative app, List lists)
        {
            if (!lists.Any<Object>()) throw new ArgumentException("Lists list must not be empty");
            Integer starterLength = ListMetrics.Pairs(GetListMetrics(lists[0]));

            if ((lists[0] as Pair).ContainsCycle && lists.Any<List>(list => !list.ContainsCycle))
                throw new ArgumentException("A list is cyclic, so they must all be");

            if (lists.Any<Object>(l => ListMetrics.Pairs(GetListMetrics(l)) != starterLength))
                throw new ArgumentException("A list does not have the same length as the others");

            HashSet<List> visitedTuples = new HashSet<List>();

            Pair resultStart, resultCurrent;

            resultStart = resultCurrent = new Pair(app.Invoke(lists.Select<Pair>(Car<Object>)));

            while (lists[0] != null && visitedTuples.Add(lists))
            {
                resultCurrent = resultCurrent.Append(app.Invoke(lists.Select<Pair>(Car<Object>)));
                lists = lists.Select<Pair>(Cdr<Pair>);
            }

            return resultStart;
        }

        #region Equality
        [Primitive("eq?", 0, true)]
        public static Boolean Eq(List objects)
        => !objects.Any<Object>() || objects.All<Object>(obj => ReferenceEquals(obj, objects[0]));

        [Primitive("equal?", 0, true)]
        public static Boolean Equal(List objects)
        => !objects.Any<Object>() || objects.All<Object>(obj => obj.Equals(objects[0]));
        #endregion

        #region Environment
        [Primitive("get-current-environment", 0)]
        public static Environment GetEnvironment() => Environment.Current;

        [Primitive("make-environment", 0, true)]
        [VariadicTypeAssertion(typeof(Environment))]
        public static Environment MakeEnvironment(List environments)
        => new Environment(environments.ToArray<Environment>());
        #endregion

        #region Booleans
        [Primitive("not?", 1)]
        [TypeAssertion(0, typeof(Boolean))]
        public static Boolean Not(Boolean boolean) => !boolean;

        [Primitive("and?", 0, true)]
        [VariadicTypeAssertion(typeof(Boolean))]
        public static Boolean And(List booleans)
        => booleans.All<Boolean>(boolean => boolean);

        [Primitive("or?", 0, true)]
        [VariadicTypeAssertion(typeof(Boolean))]
        public static Boolean Or(List booleans)
        => booleans.Any<Boolean>(boolean => boolean);
        #endregion

        [Primitive("length", 1)]
        public static Number Length(Object @object)
        => !(@object is List l) ? 0 : l.ContainsCycle ? Real.PositiveInfinity :
                                        (Number)ListMetrics.Pairs(GetListMetrics(l));

        [Primitive("list-ref", 2)]
        [TypeAssertion(0, typeof(List))]
        [TypeAssertion(1, typeof(Integer))]
        public static Object ListRef(List list, Integer i) => list[(int)i];

        [Primitive("append", 0, true)]
        [VariadicTypeAssertion(typeof(List))]
        public static List Append(List lists)
        {
            if (!lists.Any<Object>()) return Null.Instance;
            if ((lists[0] as List).ContainsCycle)
                throw new ArgumentException("Only last argument can be cyclic");
            Pair head, tail;
            head = tail = lists[0].Copy() as Pair;
            tail.Tail.Cdr = lists.Skip(1).ForEachReturnLast((Action<List>)((list) =>
            {
                if (list.ContainsCycle)
                    throw new ArgumentException("Only last argument can be cyclic");
                tail = tail.Append(list);
                if (list is Pair p) //Skip until end
                {
                    tail = p.Tail;
                }
            }));
            return head;
        }

        [Primitive("list-neighbors", 1)]
        [TypeAssertion(0, typeof(List))]
        public static List ListNeighbors(List list)
        {
            if (list is Null) return Null.Instance;

            Pair current = list as Pair;
            if (!(current.Cdr is Pair)) return Null.Instance;

            Pair result = new Pair(new Pair(current.Car, Cadr<Object>(current), Null.Instance));
            HashSet<Pair> visitedPairs = new HashSet<Pair>
                {
                    current
                };

            current = current.Cdr as Pair;
            while (current.Cdr is Pair && visitedPairs.Add(current))
            {
                result.Append(new Pair(current.Car, Cdar<Object>(current), Null.Instance));
                current = current.Cdr as Pair;
            }
            return result;

        }

        [Primitive("filter", 2)]
        [TypeAssertion(0, typeof(Applicative))]
        [TypeAssertion(1, typeof(List))]
        public static List Filter(Applicative app, List list)
        => list.Filter<Object>(obj =>
                               app.Invoke(obj) as Boolean
                               ?? throw new ArgumentException("Did not receive a Boolean from invoking applicative"));

        [Primitive("assoc", 2)]
        [TypeAssertion(1, typeof(Pair))]
        [PredicateAssertion(1, typeof(Primitives), nameof(AllPairs))]
        public static Object Assoc(Object @object, Pair p) =>
        p.FirstOrNull<Pair>(pair => pair.Car.Equals(@object));

        [Primitive("member?", 2)]
        [TypeAssertion(1, typeof(List))]
        public static Boolean Member(Object @object, List list)
        => list.Any<Object>(obj => obj.Equals(@object));

        [Primitive("finite-list?", 0, true)]
        public static Boolean FiniteList(List objects)
        => !objects.Any<Object>(@object => @object is List list && list.ContainsCycle);

        [Primitive("countable-list?", 0, true)]
        public static Boolean CountableList(List objects)
        => objects.All<Object>(@object => @object is List);

        [Primitive("reduce", 3, true)]
        [TypeAssertion(0, typeof(List))]
        [TypeAssertion(1, typeof(Applicative))]
        public static Object Reduce(List objects, Applicative binary, Object identity, List cycleApplicatives)
        {
            if (objects.ContainsCycle && !(cycleApplicatives.Count(false) == 3 && cycleApplicatives.All<Object>(obj => obj is Applicative)))
                throw new ArgumentException("Wrong call syntax. Cyclic list given but without cycle applicatives.");
            if (!objects.ContainsCycle && cycleApplicatives.Any<Object>())
                throw new ArgumentException("Wrong call syntax. Acyclic list given but extra argumens given.");
            if (objects is Null)
                return Null.Instance;
            if (objects.ContainsCycle)
                return objects.AggregateCyclic((current, next) => binary.Invoke(current, next)
                                        , (obj) => Car<Applicative>(cycleApplicatives as Pair).Invoke(obj)
                                        , (current, next) => Cadr<Applicative>(cycleApplicatives as Pair).Invoke(current, next)
                                        , (obj) => Caddr<Applicative>(cycleApplicatives as Pair).Invoke(obj)
                                        , identity);

            return objects.AggregateAcyclic((current, next) => binary.Invoke(current, next), identity);
        }

        [Primitive("append!", 0, true)]
        [VariadicTypeAssertion(typeof(List))]
        public static Inert AppendMutate(List lists)
        {
            if (lists is Null) throw new ArgumentException("Cannot append no lists");
            if (lists.Any<Pair>(((first, i)
                                 => lists.Any<Pair>(((second, j)
                                                     => i != j
                                                     && first != second
                                                     && !first.ContainsCycle
                                                     && !second.ContainsCycle
                                                     && first.Tail == second.Tail)))))
                throw new ArgumentException("A list contains a tail pair that is in a different list");
            if (lists[0] is Null || (lists[0] as List).ContainsCycle)
                throw new ArgumentException("First element must be an acyclic nonempty list");
            Pair start = lists[0] as Pair;
            Object last = lists.Skip(1).ForEachReturnLast<List>(pair =>
           {
               if (pair.ContainsCycle)
                   throw new ArgumentException("Cannot append an element that is cyclic and not last.");
               if (pair is Pair)
                   start.Tail.Cdr = pair;
           });
            start.Tail.Cdr = last;
            return Inert.Instance;
        }

        [Primitive("copy-es", 1)]
        public static Object CopyEvaluationStructure(Object @object)
        => @object is Pair pair ? new Pair(CopyEvaluationStructure(pair.Car), CopyEvaluationStructure(pair.Cdr)) : @object;

        [Primitive("assq", 2)]
        [TypeAssertion(1, typeof(List))]
        [PredicateAssertion(1, typeof(Primitives), nameof(AllPairs))]
        public static Object Assq(Object @object, List pairs)
        => pairs.FirstOrNull<Object>((obj) => ReferenceEquals(obj, @object));

        [Primitive("memq", 2)]
        [TypeAssertion(1, typeof(List))]
        public static Boolean Memq(Object @object, List list)
        => list.Any<Object>((obj) => ReferenceEquals(obj, @object));

        [Primitive("make-kernel-standard-environment")]
        public static Environment StandartEnvironment() => new Environment(Environment.Ground);

        #region Numbers

        [Primitive("+", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Number Add(List numbers)
        => AggregateNumbers(numbers, (current, start) => current + start, 0);

        [Primitive("*", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Number Multiply(List numbers)
        => AggregateNumbers(numbers, (current, start) => current * start, Integer.One);

        [Primitive("-", 1, true)]
        [TypeAssertion(0, typeof(Number))]
        [VariadicTypeAssertion(typeof(Number), 1)]
        public static Number Subtract(Number seed, List numbers)
        => numbers.Any<Object>() ? AggregateNumbers(numbers, (current, start) => current - start, seed)
                      : -seed;

        [Primitive("/", 1, true)]
        [TypeAssertion(0, typeof(Number))]
        [VariadicTypeAssertion(typeof(Number), 1)]
        public static Number Divide(Number seed, List numbers)
        => numbers.Any<Object>() ? AggregateNumbers(numbers, (current, start) => current / start, seed)
                      : Integer.One / seed;

        [Primitive("even?", 1)]
        [TypeAssertion(0, typeof(Integer))]
        public static Boolean IsEven(Integer integer) => integer % 2 == 0;
        [Primitive("mod", 2)]
        [TypeAssertion(0, typeof(Integer))]
        [TypeAssertion(1, typeof(Integer))]
        public static Integer Modulus(Integer x, Integer y) => x % y;

        [Primitive("div", 2)]
        [TypeAssertion(0, typeof(Integer))]
        [TypeAssertion(1, typeof(Integer))]
        public static Integer Divide(Integer x, Integer y) => x.Div(y);

        [Primitive("odd?", 1)]
        [TypeAssertion(0, typeof(Integer))]
        public static Boolean IsOdd(Integer integer) => integer % 2 == 1;

        [Primitive("=?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean Equals(List numbers)
        => Equal(numbers);

        [Primitive("<=?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean LessOrEqual(List numbers)
        => ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) <= Cadr<Number>(pair));

        [Primitive("<?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean Less(List numbers)
        => ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) < Cadr<Number>(pair));

        [Primitive(">?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean BiggerThan(List numbers)
        => ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) > Cadr<Number>(pair));

        [Primitive(">=?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean BiggerThanOrEqual(List numbers)
        => ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) >= Cadr<Number>(pair));

        [Primitive("max", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Number Max(List numbers)
        => AggregateNumbers(numbers, (current, next) => current > next ? current : next, Real.NegativeInfinity);

        [Primitive("min", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Number Min(List numbers)
        => AggregateNumbers(numbers, (current, next) => current > next ? next : current, Real.PositiveInfinity);

        [Primitive("gcd", 0, true)]
        [VariadicTypeAssertion(typeof(Integer))]
        public static Integer GCD(List numbers)
        => numbers is Null ? 0 : numbers.Count(false) == 1 ? numbers[0] as Integer :
                                    AggregateNumbers(numbers.Skip(1),
                                                     (current, next) => Integer.GCD(current as Integer, next as Integer),
                                                     numbers[0] as Integer) as Integer;
        [Primitive("exact?", 1)]
        [TypeAssertion(0, typeof(Number))]
        public static Boolean Exact(Number n) => n.Exact;

        [Primitive("inexact?", 1)]
        [TypeAssertion(0, typeof(Number))]
        public static Boolean Inexact(Number n) => !n.Exact;

        [Primitive("floor", 1)]
        [TypeAssertion(0, typeof(Number))]
        public static Integer Floor(Number number) => number switch
        {
            Integer i => i,
            Rational n => n.Numerator.Div(n.Denominator),
            Real n => Real.Floor(n),
            Complex _ => throw new ArgumentException("Cannot get floor of a complex number."),
            _ => throw new InvalidOperationException("WATAFAK?!"),
        };

        [Primitive("ceiling", 1)]
        [TypeAssertion(0, typeof(Number))]
        public static Integer Ceiling(Number number) => number switch
        {
            Integer i => i,
            Rational n => n.Numerator.Div(n.Denominator) + (Integer.GCD(n.Numerator, n.Denominator) == n.Denominator ? 0 : 1),
            Real n => Real.Ceiling(n),
            Complex _ => throw new ArgumentException("Cannot get floor of a complex number."),
            _ => throw new InvalidOperationException("WATAFAK?!"),
        };



        [Primitive("make-rectangular", 2)]
        [TypeAssertion(0, typeof(Number))]
        [TypeAssertion(1, typeof(Number))]
        public static Complex MakeRectangular(Number real, Number imaginary) => Complex.Get(real, imaginary);

        [Primitive("imag-part", 1)]
        [TypeAssertion(0, typeof(Complex))]
        public static Number ImaginaryPart(Complex complex)
        => complex.ImaginaryPart;

        [Primitive("real-part", 1)]
        [TypeAssertion(0, typeof(Complex))]
        public static Number RealPart(Complex complex)
        => complex.RealPart;

        [Primitive("finite?", 0, true)]
        [VariadicTypeAssertion(typeof(Number))]
        public static Boolean Finite(List numbers)
        {
            static bool Infinite(Number number)
            {
                if (number is Real r && ((!number.Exact && !Real.HasPrimaryValue(r)) || Real.IsUndefined(r)))
                    throw new ArgumentException("Argument does not have a primary value.");

                return number == Real.NegativeInfinity
                    || number == Real.PositiveInfinity
                    || (number is Real real && !number.Exact && Real.HasPrimaryValue(real))
                    || (number is Complex c && Infinite(c.RealPart) && Infinite(c.ImaginaryPart));
            }

            return numbers.All<Number>(Infinite);
        }

        [Primitive("integer?", 0, true)]
        public static Boolean IsInteger(List objects)
        {
            static bool isInteger(Object @object) => @object switch
            {
                Integer integer => true,
                Rational rational => rational.Denominator == 1,
                Real real => real == Floor(real),
                Complex complex => ReferenceEquals(complex.ImaginaryPart, Integer.Zero) && isInteger(complex.RealPart),
                _ => false,
            };
            return objects.All<Object>(isInteger);
        }

        static Number AggregateNumbers(List numbers, Func<Number, Number, Number> action, Number seed)
        {
            static Number identity(Number x) => x;
            return numbers.ContainsCycle
                ? numbers.AggregateCyclic(action, identity, action, identity, seed)
                : numbers.AggregateAcyclic(action, seed);
        }
        #endregion

        [Primitive("make-encapsulation-type")]
        public static List MakeEncapsulationType()
        {
            Guid guid = Guid.NewGuid();

            Boolean SameEncapsulations(List input)
            => input.All<Object>((obj) => obj is Encapsulation capsule && capsule.Identificator == guid);
            Encapsulation Create(List input)
            {
                if (input.Count(false) != 1)
                    throw new ArgumentException("Applicative requires one argument.");
                return new Encapsulation(input[0], guid);
            }

            Object Open(List input)
            {
                if (input.Count(false) != 1 || !(input[0] is Encapsulation capsule))
                    throw new ArgumentException("Applicative requires one argument which is an encapsulation.");
                return capsule.Open(guid, "Encapsulation is not from the same type.");
            }

            Applicative e = new Applicative(Create);
            Applicative p = new Applicative(SameEncapsulations);
            Applicative d = new Applicative(Open);
            return new Pair(e, p, d);
        }

        [Primitive("exit")]
        public static Inert Exit()
        {
            System.Environment.Exit(0);
            return Inert.Instance;
        }

        [Primitive("force", 1)]
        public static Object Force(Object @object)
        {
            while (@object is Promise lazy)
                @object = lazy.Evaluate();
            return @object;
        }

        [Primitive("memoize", 1)]
        public static Promise Memoize(Object value)
        => new Promise(value);

        [Primitive("newline", 1)]
        [TypeAssertion(0, typeof(Port), true)]
        [PredicateAssertion(0, typeof(Primitives), "IsOutputPort", true, true)]
        public static Inert NewLine(Port p)
        {
            p ??= Port.StandardOutput;
            p.Writer.WriteLine();
            return Inert.Instance;
        }

        [Primitive("load", 1)]
        [TypeAssertion(0, typeof(String))]
        public static Inert Load(String fileName)
        {
            if (!System.IO.File.Exists(fileName))
                throw new ArgumentException($"File {fileName} does not exist.");
            using System.IO.StreamReader reader = new System.IO.StreamReader(fileName);
            Operatives.Sequence(Environment.Current, Parser.Parser.Parse($"({reader.ReadToEnd()})") as List);
            return Inert.Instance;
        }


        [Primitive("make-keyed-static-variable")]
        public static Pair MakeKeyedStaticVariable()
        {
            Guid guid = Guid.NewGuid();
            Applicative b = new Applicative((input) =>
            {
                if (input.Count(false) != 2)
                    throw new ArgumentException("Applicative requires two arguments");
                if (!(input[1] is Environment env))
                    throw new ArgumentException("Second argument must be a environment");
                return new Environment(env, guid, input[0]);
            });
            Applicative a = new Applicative((input) =>
            {
                if (input.Count(false) != 0)
                    throw new ArgumentException("Applicative requires no arguments");
                Environment constructed;
                if ((constructed = Environment.Current
                     .ImproperParents
                     .FirstOrDefault(parent => parent.ID == guid)) != null)
                    return constructed.Value;

                else throw new ArgumentException("Current environment has no parent that is generated from this static variable");
            });
            return new Pair(b, a);
        }

        [Primitive("symbol->string", 1)]
        [TypeAssertion(0, typeof(Symbol))]
        public static String SymbolToString(Symbol s) => String.Get(s.Data);

        [Primitive("string->symbol", 1)]
        [TypeAssertion(0, typeof(String))]
        public static Symbol StringToSymbol(String s) => Symbol.Get(s.Data);

        [Primitive("input-port?", 0, true)]
        public static Boolean IsInputPort(List objects)
        => objects.All<Object>((obj) => obj is Port p && p.Type == PortType.Input);

        [Primitive("output-port?", 0, true)]
        public static Boolean IsOutputPort(List objects)
        => objects.All<Object>((obj) => obj is Port p && p.Type == PortType.Output);

        [Primitive("current-input-port")]
        public static Port CurrentInputPort() => Port.CurrentInput;

        [Primitive("current-output-port")]
        public static Port CurrentOutputPort() => Port.CurrentOutput;

        [Primitive("current-error-port")]
        public static Port CurrentErrorPort() => Port.CurrentError;

        [Primitive("open-input-file", 1)]
        [TypeAssertion(0, typeof(String))]
        public static Port OpenInputFile(String fileName)
        => new Port(fileName, PortType.Input);

        [Primitive("open-output-file", 1)]
        [TypeAssertion(0, typeof(String))]
        public static Port OpenOutputFile(String fileName)
        => new Port(fileName, PortType.Output);

        [Primitive("close-input-port", 1)]
        [TypeAssertion(0, typeof(Port))]
        [PredicateAssertion(0, typeof(Primitives), "IsInputPort")]
        public static Inert CloseInputPort(Port p)
        {
            p.Dispose();
            return Inert.Instance;
        }

        [Primitive("close-output-port", 1)]
        [TypeAssertion(0, typeof(Port))]
        [PredicateAssertion(0, typeof(Primitives), "IsOutputPort")]
        public static Inert CloseOutputPort(Port p)
        {
            p.Dispose();
            return Inert.Instance;
        }

        [Primitive("read-line", 1)]
        [TypeAssertion(0, typeof(Port))]
        [PredicateAssertion(0, typeof(Primitives), "IsInputPort")]
        public static String ReadLine(Port p) => p.Reader.ReadLine();

        [Primitive("write-line", 2)]
        [TypeAssertion(0, typeof(Port))]
        [PredicateAssertion(0, typeof(Primitives), "IsOutputPort")]
        [TypeAssertion(1, typeof(String))]
        public static Inert WriteLine(Port p, String s)
        {
            p.Writer.WriteLine(s);
            p.Writer.Flush();
            return Inert.Instance;
        }
        [Primitive("write", 2)]
        [TypeAssertion(0, typeof(Port))]
        [PredicateAssertion(0, typeof(Primitives), "IsOutputPort")]
        public static Inert WriteLine(Port p, Object o)
        {
            p.Writer.WriteLine(o);
            p.Writer.Flush();
            return Inert.Instance;
        }
    }
}