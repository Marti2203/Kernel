#define FastCopyES
using System;
using System.Collections.Generic;
using System.Reflection;
using Kernel.Combiners;
using Kernel.Arithmetic;
using static Kernel.Primitives.DynamicConnections;
using static CarFamily;
using Kernel.Utilities;
using System.Linq;
namespace Kernel.Primitives
{
    public static class Primitives
    {
        public static Combiner Get(string name)
        => functions.ContainsKey(name) ? functions[name] : throw new NoBindingException("No such primitive");
        public static bool Has(string name)
        => functions.ContainsKey(name);

        static readonly IDictionary<string, Combiner> functions;

        static Primitives()
        {
            functions = new Dictionary<string, Combiner>();
            AddPredicateApplicatives();
            AddApplicatives();
            AddCarFamily();
            AddOperatives();
        }



        public static bool ValidBindingList(Object obj)
        => obj is List l && l.All<Object>(element => element is Pair pair && IsFormalParameterTree(pair.Car) && !(pair.Cdr is Null));

        public static bool UniqueBindingList(Object obj)
        => obj is List bindings && bindings.Select<Pair>(Car<Object>).All<Object>(new HashSet<Object>().Add);

        public static bool AllPairs(Object obj) => obj is Pair p && p.All<Object>(@object => @object is Pair);

        public static bool AllSymbols(Object obj) => obj is Pair p && p.All<Object>(@object => @object is Symbol);

        public static bool ContainsCycle(Object obj)
        => obj is List l && l.ContainsCycle;

        public static Object Evaluate(Object @object, Environment environment)
        {
            Environment temp = Environment.Current;
            Environment.Current = environment;
            Object result = environment.Evaluate(@object);
            Environment.Current = temp;
            return result;
        }

        public static bool IsFormalParameterTree(Object @object) => IsFormalParameterTree(@object, true);

        public static void Match(Environment env, Object definiend, Object result)
        {
            switch (definiend)
            {
                case Symbol s:
                    env[s] = result;
                    break;
                case Null n:
                    if (!(result is Null))
                        throw new ArgumentException("Expression must be null");
                    break;
                case Pair pDefiniend:
                    if (!(result is Pair pExpression))
                        throw new ArgumentException("Expression must be a pair");
                    Match(env, pDefiniend.Car, pExpression.Car);
                    Match(env, pDefiniend.Cdr, pExpression.Cdr);
                    break;
            }
        }

        static bool NullOrEnvironment(Object @object) => @object is Null || @object is Environment;

        static bool IsFormalParameterTree(Object @object, bool pairCheck)
        => @object is Symbol
        || @object is Ignore
        || @object is Null
        || (pairCheck && @object is Pair p && IsFormalParameterTree(p));

        static bool IsFormalParameterTree(Pair p)
        {
            HashSet<Symbol> containedSymbols = new HashSet<Symbol>();
            Stack<Pair> pairs = new Stack<Pair>();
            pairs.Push(p);
            do
            {
                p = pairs.Pop();
                if (p.ContainsCycle) throw new ArgumentException("Cannot accept a cyclic tree");
                if (p.Car is Pair pCar)
                    pairs.Push(pCar);
                else
                {
                    if (p.Car is Symbol sCar && !containedSymbols.Add(sCar))
                        throw new ArgumentException($"Parameter tree contains a duplicate of symbol {sCar}");
                    if (!IsFormalParameterTree(p.Car, false))
                        return false;
                }
                if (p.Cdr is Pair pCdr)
                    pairs.Push(pCdr);
                else
                {
                    if (p.Cdr is Symbol sCdr && !containedSymbols.Add(sCdr))
                        throw new ArgumentException($"Parameter tree contains a duplicate of symbol {sCdr}");
                    if (!IsFormalParameterTree(p.Cdr, false))
                        return false;
                }
            }
            while (pairs.Count != 0);
            return true;
        }

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

            [Primitive("display", 1)]
            public static Inert Display(Object obj)
            {
                Console.WriteLine(obj);
                return Inert.Instance;
            }

            [Primitive("read")]
            public static Object Read()
            {
                bool Valid(string representation)
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

            [Primitive("apply", 2, true)]
            [TypeAssertion(0, typeof(Applicative))]
            public static Object Apply(Applicative a, Object p, List objects)
            {
                int count = objects.Count(false);
                if (count > 1 || count < 0)
                    throw new ArgumentException("Apply can except an additional environment only", nameof(objects));
                if (count == 1)
                {
                    if (objects[0] is Environment e)
                        return Evaluate(new Pair(a.Combiner, p), e);

                    throw new ArgumentException("Extra argument has to be an environment.", nameof(objects));
                }

                List list = p is List l ? l : new Pair(p);
                return Environment.Current.Evaluate(a.Combiner, list);
            }
            public static class ListMetrics
            {
                public static Integer Pairs(List metric)
                => metric is Null ? 0 : Car<Integer>(metric as Pair);
                public static Boolean WithNull(List metric)
                => metric is Null || Cdar<Integer>(metric as Pair) != 0;
                public static Boolean Cyclic(List metric)
                => !(metric is Null) && Cdddar<Integer>(metric as Pair) != 0;
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
            => (!objects.Any<Object>() || objects.All<Object>(obj => ReferenceEquals(obj, objects[0])));

            [Primitive("equal?", 0, true)]
            public static Boolean Equal(List objects)
            => (!objects.Any<Object>() || objects.All<Object>(obj => obj.Equals(objects[0])));
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
                Pair head, tail;
                if ((lists[0] as List).ContainsCycle)
                    throw new ArgumentException("Only last argument can be cyclic");
                head = tail = lists[0].Copy() as Pair;
                List last = lists.Skip(1).ForEachReturnLast<List>((list) =>
                {
                    if (list.ContainsCycle)
                        throw new ArgumentException("Only last argument can be cyclic");
                    list.ForEach<Object>(obj => tail = tail.Append(obj));
                });
                tail.Tail.Cdr = last;
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

            //[Primitive("+", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Number Add(List numbers)
            //=> AggregateNumbers(numbers, (current, start) => current + start, 0);

            //[Primitive("*", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Number Multiply(List numbers)
            //=> AggregateNumbers(numbers, (current, start) => current * start, Integer.One);

            //[Primitive("-", 1, true)]
            //[TypeAssertion(0, typeof(Number))]
            //[VariadicTypeAssertion(typeof(Number), 1)]
            //public static Number Subtract(Number seed, List numbers)
            //=> numbers.Any<Object>() ? AggregateNumbers(numbers, (current, start) => current - start, seed)
            //              : -seed;


            //[Primitive("/", 1, true)]
            //[TypeAssertion(0, typeof(Number))]
            //[VariadicTypeAssertion(typeof(Number), 1)]
            //public static Number Divide(Number seed, List numbers)
            //=> numbers.Any<Object>() ? AggregateNumbers(numbers, (current, start) => current / start, seed)
            //              : Integer.One / seed;

            //[Primitive("even?", 1)]
            //[TypeAssertion(0, typeof(Integer))]
            //public static Boolean IsEven(Integer integer) => integer % 2 == 0;

            //[Primitive("=?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean Equals(List numbers)
            //=> Equal(numbers);

            //[Primitive("<=?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean LessOrEqual(List numbers)
            //=> ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) <= Cadr<Number>(pair));

            //[Primitive("<?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean Less(List numbers)
            //=> ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) < Cadr<Number>(pair));

            //[Primitive(">?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean BiggerThan(List numbers)
            //=> ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) > Cadr<Number>(pair));

            //[Primitive(">=?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean BiggerThanOrEqual(List numbers)
            //=> ListNeighbors(numbers).All<Pair>((pair) => Car<Number>(pair) >= Cadr<Number>(pair));

            //[Primitive("max", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Number Max(List numbers)
            //=> AggregateNumbers(numbers, (current, next) => current > next ? current : next, Real.NegativeInfinity);

            //[Primitive("min", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Number Min(List numbers)
            //=> AggregateNumbers(numbers, (current, next) => current > next ? next : current, Real.PositiveInfinity);

            //[Primitive("gcd", 0, true)]
            //[VariadicTypeAssertion(typeof(Integer))]
            //public static Integer GCD(List numbers)
            //=> numbers is Null ? 0 : numbers.Count(false) == 1 ? numbers[0] as Integer :
            //                            AggregateNumbers(numbers.Skip(1),
            //                                             (current, next) => Integer.GCD(current as Integer, next as Integer),
            //                                             numbers[0] as Integer) as Integer;
            //[Primitive("exact?", 1)]
            //[TypeAssertion(0, typeof(Number))]
            //public static Boolean Exact(Number n) => n.Exact;

            //[Primitive("inexact?", 1)]
            //[TypeAssertion(0, typeof(Number))]
            //public static Boolean Inexact(Number n) => !n.Exact;

            //[Primitive("floor", 1)]
            //[TypeAssertion(0, typeof(Number))]
            //public static Integer Floor(Number number)
            //{
            //    switch (number)
            //    {
            //        case Integer i:
            //            return i;
            //        case Rational n:
            //            return n.Numerator.Div(n.Denominator);
            //        case Real n:
            //            return Real.Floor(n);
            //        case Complex c:
            //            throw new ArgumentException("Cannot get floor of a complex number.");
            //    }
            //    throw new InvalidOperationException("WATAFAK?!");
            //}

            //[Primitive("ceiling", 1)]
            //[TypeAssertion(0, typeof(Number))]
            //public static Integer Ceiling(Number number)
            //{
            //    switch (number)
            //    {
            //        case Integer i:
            //            return i;
            //        case Rational n:
            //            return n.Numerator.Div(n.Denominator) + (Integer.GCD(n.Numerator, n.Denominator) == n.Denominator ? 0 : 1);
            //        case Real n:
            //            return Real.Ceiling(n);
            //        case Complex c:
            //            throw new ArgumentException("Cannot get floor of a complex number.");
            //    }
            //    throw new InvalidOperationException("WATAFAK?!");
            //}

            //[Primitive("make-rectangular", 2)]
            //[TypeAssertion(0, typeof(Number))]
            //[TypeAssertion(1, typeof(Number))]
            //public static Complex MakeRectangular(Number real, Number imaginary) => Complex.Get(real, imaginary);

            //[Primitive("imag-part", 1)]
            //[TypeAssertion(0, typeof(Complex))]
            //public static Number ImaginaryPart(Complex complex)
            //=> complex.Imaginary;

            //[Primitive("real-part", 1)]
            //[TypeAssertion(0, typeof(Complex))]
            //public static Number RealPart(Complex complex)
            //=> complex.RealPart;

            //[Primitive("finite?", 0, true)]
            //[VariadicTypeAssertion(typeof(Number))]
            //public static Boolean Finite(List numbers)
            //{
            //    bool Infinite(Number number)
            //    {
            //        if (number is Real r && !number.Exact && !Real.HasPrimaryValue(r) || Real.IsUndefined(r))
            //            throw new ArgumentException("Argument does not have a primary value.");

            //        return number == Real.NegativeInfinity || number == Real.PositiveInfinity
            //                             || (number is Real real && !number.Exact && Real.HasPrimaryValue(real))
            //                             || (number is Complex c && Infinite(c.RealPart) && Infinite(c.Imaginary));
            //    }

            //    return numbers.All<Number>(Infinite);
            //}

            //[Primitive("integer?", 0, true)]
            //public static Boolean IsInteger(List objects)
            //{
            //    bool isInteger(Object @object)
            //    {
            //        switch (@object)
            //        {
            //            case Integer integer:
            //                return true;
            //            case Rational rational:
            //                return rational.Denominator == 1;
            //            case Real real:
            //                return real == Real.Get(1d);
            //            case Complex complex:
            //                return complex.Imaginary == Integer.Zero && isInteger(complex.RealPart);
            //            default:
            //                return false;
            //        }
            //    }
            //    return objects.All<Object>(isInteger);
            //}

            static Number AggregateNumbers(List numbers, Func<Number, Number, Number> action, Number seed)
            {
                Func<Number, Number> identity = (x) => x;
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

            [Primitive("newline")]
            public static Inert NewLine()
            {
                Console.WriteLine();
                return Inert.Instance;
            }

            [Primitive("load", 1)]
            [TypeAssertion(0, typeof(String))]
            public static Inert Load(String fileName)
            {
                if (!System.IO.File.Exists(fileName))
                    throw new ArgumentException($"File {fileName} does not exist in the working directory");
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileName))
                {
                    Operatives.Sequence(Environment.Current, Parser.Parser.Parse($"({reader.ReadToEnd()})") as List);
                }
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
        }

        public static class Operatives
        {
            [Primitive("$if", 4)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object If(Environment env, Object testInput, Object consequent, Object alternative)
            {
                Object test = Evaluate(testInput, env);
                if (!(test is Boolean condition)) throw new ArgumentException("Test is not a boolean.");

                return Evaluate(condition ? consequent : alternative, env);
            }

            [Primitive("$define!", 3)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(1, typeof(Primitives), nameof(IsFormalParameterTree))]
            public static Inert Define(Environment env, Object definiend, Object expr)
            {
                Match(env, definiend, env.Evaluate(expr));
                return Inert.Instance;
            }

            [Primitive("$vau", 3, true)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(1, typeof(Primitives), nameof(IsFormalParameterTree))]
            [TypeAssertion(2, typeof(Symbol), typeof(Ignore))]
            public static Operative Vau(Environment env, Object formals, Object eformal, List expr)
            {
                if ((formals is Symbol && formals == eformal)
                    || (formals is Pair p && eformal is Symbol && p.Contains(eformal)))
                    throw new ArgumentException("Formals contain eformal");

                return new Operative(env,
                                     Applicatives.CopyEvaluationStructureImmutable(formals),
                                     eformal,
                                     expr.Select<Object>(Applicatives.CopyEvaluationStructureImmutable));

            }

            [Primitive("$lambda", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Applicative Lambda(Environment env, Object formals, List exprs)
            => Applicatives.Wrap(Vau(env, formals, Ignore.Instance, exprs));


            [Primitive("$sequence", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object Sequence(Environment env, List objects)
            => !objects.Any<Object>() ? Inert.Instance
                           : Evaluate(objects.ForEachReturnLast<Object>((obj) => Evaluate(obj, env)), env);

            [Primitive("$cond", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            [VariadicTypeAssertion(typeof(Pair), 1)]
            public static Object Cond(Environment env, List objects)
            {
                Object result = Inert.Instance;
                objects.ForEach<Pair>(p =>
                {
                    Object test = env.Evaluate(p.Car);
                    if (!(test is Boolean condition))
                        throw new ArgumentException($"Test in {p.Car} is not a boolean.");

                    if (condition)
                    {
                        result = Sequence(env, (p.Cdr as Pair));
                        return true;
                    }
                    return false;
                });
                return result;
            }

            [Primitive("$let", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(ValidBindingList))]
            [PredicateAssertion(1, typeof(Primitives), nameof(UniqueBindingList))]
            public static Object Let(Environment environment, List bindings, List body)
            {
                Environment child = new Environment(environment);
                bindings.ForEach<Pair>(binding =>
                {
                    Object expression = Cadr<Object>(binding);
                    Match(child, binding.Car, Evaluate(expression, environment));
                });
                return Sequence(child, body);
            }

            [Primitive("$let*", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(ValidBindingList))]
            public static Object LetStar(Environment environment, List bindings, List body)
            {
                Environment child = new Environment(environment);
                bindings.ForEach<Pair>(binding =>
                {
                    Object expression = Cadr<Object>(binding);
                    Match(child, binding.Car, Evaluate(expression, child));
                    child = new Environment(child);
                });
                return Sequence(child, body);
            }

            [Primitive("$letrec", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(ValidBindingList))]
            [PredicateAssertion(1, typeof(Primitives), nameof(UniqueBindingList))]
            public static Object LetRec(Environment environment, List bindings, List body)
            {
                Environment child = new Environment(environment);
                bindings.ForEach<Pair>(binding =>
                {
                    Object expression = Cadr<Object>(binding);
                    Match(child, binding.Car, Evaluate(expression, child));
                });
                return Sequence(child, body);
            }

            [Primitive("$letrec*", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(ValidBindingList))]
            public static Object LetRecStar(Environment environment, List bindings, List body)
            {
                Environment child = new Environment(environment);
                bindings.ForEach<Pair>(binding =>
                {
                    Object expression = Cadr<Object>(binding);
                    Match(child, binding.Car, Evaluate(expression, child));
                    child = new Environment(child);
                });
                return Sequence(child, body);
            }

            [Primitive("$let-redirect", 3, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(2, typeof(List))]
            [PredicateAssertion(2, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(2, typeof(Primitives), nameof(ValidBindingList))]
            public static Object LetRedirect(Environment environment, Object exp, List bindings, List body)
            => (Evaluate(exp, environment) is Environment env) ?
            Lambda(env, bindings.Select<Pair>(Car<Object>), body)
                .Invoke(bindings.Select<Pair>((binding) => Evaluate(Cdar<Object>(binding), environment))) :
                throw new ArgumentException("Expression did not evaluate to an environment");

            [Primitive("$let-safe", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(ValidBindingList))]
            public static Object LetSafe(Environment environment, List bindings, List body)
            => LetRedirect(environment, Applicatives.StandartEnvironment(), bindings, body);

            [Primitive("$and?", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object And(Environment environment, List objects)
            => Evaluate(objects.FirstValidOrLast<Object>((obj) =>
                {
                    Object temp = Evaluate(obj, environment);
                    if (!(temp is Boolean))
                        throw new ArgumentException($"Argument {obj} does not evaluate to a boolean");
                    return temp == Boolean.False ? Boolean.False : null;
                }), environment);

            [Primitive("$or?", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object Or(Environment environment, List objects)
            => Evaluate(objects.FirstValidOrLast<Object>((obj) =>
               {
                   Object temp = Evaluate(obj, environment);
                   if (!(temp is Boolean))
                       throw new ArgumentException($"Argument {obj} does not evaluate to a boolean");
                   return temp == Boolean.True ? Boolean.True : null;
               }), environment);

            [Primitive("$defined?", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            [VariadicTypeAssertion(typeof(Symbol), 1)]
            public static Boolean Defined(Environment environment, List symbols)
            => symbols.All<Symbol>(environment.Contains);

            [Primitive("$binds?", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [VariadicTypeAssertion(typeof(Symbol), 2)]
            public static Boolean Binds(Environment environment, Object expr, List symbols)
            => environment.Evaluate(expr) is Environment e ? symbols.All<Symbol>(e.Contains) :
                          throw new ArgumentException($"{expr} does not evaluate to an environment");

            [Primitive("$remote-eval", 3)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object RemoteEval(Environment environment, Object exp1, Object exp2)
            => Evaluate(exp2, environment) is Environment env ? Evaluate(exp1, env)
                : throw new ArgumentException($"{exp2} does not evaluate to an environment");

            [Primitive("$bindings->environment", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Environment BindingsToEnvironment(Environment environment, List bindings)
            => LetRedirect(environment,
                           new Environment(Array.Empty<Environment>()),
                           bindings,
                           new Pair(Symbol.Get("get-current-environment"))) as Environment;

            [Primitive("$set!", 4)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(2, typeof(Primitives), nameof(IsFormalParameterTree))]
            public static Inert Set(Environment environment, Object exp1, Object formals, Object exp2)
            {
                if (!(environment.Evaluate(exp1) is Environment env))
                    throw new ArgumentException("First argument did not evaluate to an environment");
                Object @object = environment.Evaluate(exp2);
                Match(env, formals, @object);
                return Inert.Instance;
            }

            [Primitive("$provide!", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, typeof(Primitives), nameof(ContainsCycle), false)]
            [PredicateAssertion(1, typeof(Primitives), nameof(AllSymbols))]
            public static Inert Provide(Environment environment, List symbols, List body)
            {
                if (body.ContainsCycle)
                    throw new ArgumentException("Body cannot be a cyclic list.");
                Environment child = new Environment(environment);
                body.ForEach<Object>((exp) => Evaluate(exp, child));
                symbols.ForEach<Symbol>((symbol) => environment[symbol] = child[symbol]);
                return Inert.Instance;
            }

            [Primitive("$import!", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [VariadicTypeAssertion(typeof(Environment), 2)]
            public static Inert Import(Environment environment, Object exp, List symbols)
            {
                if (!(Evaluate(exp, environment) is Environment env))
                    throw new ArgumentException("First argument does not evaluate to an environment");
                return Define(environment, symbols, RemoteEval(env, new Pair(symbols), exp));
            }

            [Primitive("$lazy", 2)]
            [TypeAssertion(0, typeof(Environment))]
            public static Promise Lazy(Environment environment, Object exp)
            => new Promise(environment, exp);

        }

        #region Primitive Generation

        static void AddApplicatives()
        {
            foreach (MethodInfo method in typeof(Applicatives)
                     .GetMethods()
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                var pipedMethod = CreatePipeFunction(method);
                Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
                functions.Add(primitiveInformation.PrimitiveName, app);
            }
        }

        static void AddOperatives()
        {
            foreach (MethodInfo method in typeof(Operatives)
                     .GetMethods()
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                Operative app = new Operative(CreatePipeFunction(method), primitiveInformation.PrimitiveName);
                functions.Add(primitiveInformation.PrimitiveName, app);
            }
        }

        static void AddPredicateApplicatives()
        {
            foreach (Type t in typeof(Object)
                     .Assembly
                     .GetTypes()
                     .Where(t => t.IsSubclassOf(typeof(Object)) && !t.IsAbstract && !t.IsGenericType))
                functions.Add(t.Name.ToLower() + "?", typeof(PredicateApplicative<>)
                              .MakeGenericType(t)
                              .GetProperty("Instance")
                              .GetValue(null) as Combiner);
        }

        /// <summary>
        /// Optimised version of Add(Applicatives|Operatives) for the car methods
        /// </summary>
        static void AddCarFamily()
        {
            foreach (MethodInfo method in typeof(CarFamily)
                     .GetMethods()
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object)))
                     .Select((method) => method.MakeGenericMethod(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                var pipedMethod = CreatePipeFunction(method);
                Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
                functions.Add(primitiveInformation.PrimitiveName, app);
            }
        }
        #endregion
    }
}