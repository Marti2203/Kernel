#define FastCopyES
using System;
using System.Collections.Generic;
using System.Reflection;
using Kernel.Combiners;
using Kernel.Arithmetic;
using static Kernel.Primitives.DynamicConnections;
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



        public static bool IsTailContext(Object obj) // Be careful with this function. I am not very fond of it. ~ Martin
        => (obj is Pair p && Environment.Current.Evaluate(p.Car) is Combiner && p.Cdr is List)
        || !(obj is Pair);

        public static bool ValidBindingList(Object obj)
        => obj is List l && l.All<Object>(element => element is Pair pair && IsFormalParameterTree(pair.Car) && !(pair.Cdr is Null));

        public static bool IsCyclic(Object obj)
        => obj is List l && l.IsCyclic;

        public static Object Evaluate(Object @object, Environment environment)
        => environment.Evaluate(@object);

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
                if (p.IsCyclic) throw new ArgumentException("Cannot accept a cyclic tree");
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
            [Primitive("each", 1, true)]
            [TypeAssertion(0, typeof(Combiner))]
            public static Inert Each(Combiner combiner, List input)
            {
                input.ForEach<Object>(obj =>
                {
                    combiner.Invoke(obj);
                });
                return Inert.Instance;
            }

            [Primitive("display", 1)]
            public static Inert Display(Object input)
            {
                Console.WriteLine(input);
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
            public static Object Cons(Object car, Object cdr) => new Pair { Car = car, Cdr = cdr };

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

            [Primitive("copy-es", 1)]
            // Todo This may have to be reworked
            public static Object CopyEvaluationStructure(Object obj)
            {
                if (!(obj is Pair p)) return obj;
                return new Pair(CopyEvaluationStructureImmutable(p.Car),
                                CopyEvaluationStructureImmutable(p.Cdr) /*, i do not know yet */ );
            }

            [Primitive("eval", 2)]
            [TypeAssertion(1, typeof(Environment))]
            [PredicateAssertion(0, nameof(IsTailContext))]
            public static Object Eval(Object tailContext, Environment env) => env.Evaluate(tailContext);

            [Primitive("wrap", 1)]
            [TypeAssertion(0, typeof(Combiner))]
            public static Combiner Wrap(Combiner c) => new Applicative(c);

            [Primitive("unwrap", 1)]
            [TypeAssertion(0, typeof(Applicative))]
            public static Combiner Unwrap(Applicative app) => app.combiner;

            [Primitive("list", 0, true)]
            public static Object List(List objects) => objects;

            [Primitive("list*", 0, true)]
            public static Object ListStar(List objects)
            {
                if (!objects.Any<Object>()) return Null.Instance;
                if (objects.Count() == 1) return objects[0];
                Pair head = new Pair(objects[0], Null.Instance);
                Pair tail = head;
                for (int i = 1; i < objects.Count() - 1; i++)
                {
                    tail = tail.Append(objects[i]);
                }
                tail.Cdr = objects[objects.Count() - 1];
                return head;
            }


            [Primitive("apply", 2, true)]
            [TypeAssertion(0, typeof(Applicative))]
            public static Object Apply(Applicative a, Object p, List objects)
            {
                if (objects.Count() > 1)
                    throw new ArgumentException("Apply can except an additional environmnt only", nameof(objects));
                if (objects.Count() == 1)
                {
                    if (objects[0] is Environment e)
                        return e.Evaluate(new Pair(a.combiner, p));
                    else
                        throw new ArgumentException("Extra argument has to be an environment.", nameof(objects));
                }
                List list;
                if (p is List l)
                    list = l;
                else list = new Pair(p);
                return Environment.Current.Evaluate(new Pair(a.combiner, list));
            }
            public static class ListMetrics
            {
                public static Integer Pairs(List metric)
                => metric is Null ? 0 : CarFamily.Car(metric as Pair) as Integer;
                public static Boolean WithNull(List metric)
                => metric is Null || ((CarFamily.Cdar(metric as Pair) as Integer) != 0);
                public static Boolean Cyclic(List metric)
                => !(metric is Null) && ((CarFamily.Cdddar(metric as Pair) as Integer) != 0);
            }

            static readonly Dictionary<Object, Pair> immutableMetricsCache = new Dictionary<Object, Pair>();
            [Primitive("get-list-metrics", 1)]
            public static Pair GetListMetrics(Object obj)
            {
                if (!obj.Mutable && immutableMetricsCache.ContainsKey(obj))
                    return immutableMetricsCache[obj];
                int pairs = 0;
                int nilCount = 0;
                int acyclicLength = 0;
                int cycleLength = 0;
                if (obj is Pair p)
                {
                    Dictionary<Pair, int> visits = new Dictionary<Pair, int>();
                    while (p != null)
                    {
                        if (visits.TryGetValue(p, out int visitsCount) && visitsCount == 2)
                            break;
                        visits.Add(p, 1);
                        if (p.Cdr is Pair pNew)
                            p = pNew;
                        else
                        {
                            if (p.Cdr is Null) nilCount++;
                            break;
                        }
                    }
                    acyclicLength = visits.Count(v => v.Value == 1) - 1;
                    cycleLength = visits.Count(v => v.Value == 2);
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
                if (i2 == Integer.Zero)
                    return Inert.Instance;

                Object start = ListTail(obj, (i1 + i2 - Integer.One) as Integer);

                if (!(start is Pair p))
                    throw new ArgumentException("List is not deep enough to encycle");

                SetCdr(p, ListTail(obj, i1));

                return Inert.Instance;
            }


            [Primitive("map", 1, true)]
            [TypeAssertion(0, typeof(Applicative))]
            [VariadicTypeAssertion(typeof(Pair), 1)]
            public static Object Map(Applicative app, List lists)
            {
                // TODO WHAT THE FUCK DO WE DO WITH CYCLIC LISTS?!!!?!?!
                // Rework
                if (lists.Count() == 0) throw new InvalidOperationException("Lists list must not be empty");
                Integer starterLength = ListMetrics.Pairs(GetListMetrics(lists[0]));

                if (lists.Count() > 1 && lists.Any<Object>(l => ListMetrics.Pairs(GetListMetrics(l)) != starterLength))
                    throw new InvalidOperationException("A list does not have the same length as the others");

                if ((lists[0] as Pair).IsCyclic && lists.Any<List>(list => list.IsCyclic))
                    throw new InvalidOperationException("A list is cyclic, so they must all be");
                Pair result = new Pair();
                while (lists[0] != null)
                {
                    if (app.combiner is Operative o)
                        result.Append(o.Invoke(lists.Select<Pair>(pair => pair.Car), Environment.Current));
                    if (app.combiner is Applicative ap)
                        result.Append(ap.Invoke(lists.Select<Pair>(pair => pair.Car)));
                    lists = lists.Select<Pair>(x => x.Cdr as Pair);
                }
                return result;
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
            {
                if (!(@object is List l))
                    return Integer.Zero;
                if (l.IsCyclic)
                    return Real.PositiveInfinity;
                return ListMetrics.Pairs(GetListMetrics(l));
            }

            [Primitive("list-ref", 2)]
            [TypeAssertion(0, typeof(List))]
            [TypeAssertion(1, typeof(Integer))]
            public static Object ListRef(List list, Integer i) => list[(int)i];

            [Primitive("append", 0, true)]
            [VariadicTypeAssertion(typeof(List))]
            public static List Append(List lists)
            {
                if (!lists.Any<Object>()) return Null.Instance;
                Pair start, result;
                if ((lists[0] as List).IsCyclic)
                    throw new ArgumentException("Only last argument can be cyclic");
                start = result = new Pair(lists[0] as IEnumerable<Object>);
                for (int i = 1; i < lists.Count() - 1; i++)
                {
                    if ((lists[i] as List).IsCyclic)
                        throw new ArgumentException("Only last argument can be cyclic");
                    (lists[i] as List).ForEach<Object>(obj => result = result.Append(obj));
                }
                result.Cdr = lists.Last();
                return start;
            }
        }
        public static class Operatives
        {
            [Primitive("$if", 4)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(2, nameof(IsTailContext))]
            [PredicateAssertion(3, nameof(IsTailContext))]
            public static Object If(Environment env, Object testInput, Object consequent, Object alternative)
            {
                Object test = env.Evaluate(testInput);
                if (!(test is Boolean condition)) throw new ArgumentException("Test is not a boolean.");

                return env.Evaluate(condition ? consequent : alternative);
            }

            [Primitive("$define", 3)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(1, nameof(IsFormalParameterTree))]
            public static Inert Define(Environment env, Object definiend, Object expr)
            {
                Match(env, definiend, env.Evaluate(expr));
                return Inert.Instance;
            }

            [Primitive("$vau", 3, true)]
            [TypeAssertion(0, typeof(Environment))]
            [PredicateAssertion(1, nameof(IsFormalParameterTree))]
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
            public static Combiner Lambda(Environment env, Object formals, List exprs)
            => Applicatives.Wrap(Vau(Environment.Current, formals, Ignore.Instance, exprs));


            [Primitive("$sequence", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object Sequence(Environment env, List objects)
            {
                if (!objects.Any<Object>()) return Inert.Instance;
                for (int i = 0; i < objects.Count() - 1; i++)
                    env.Evaluate(objects[i]);
                if (IsTailContext(objects[objects.Count() - 1]))
                    return env.Evaluate(objects[objects.Count() - 1]);
                throw new ArgumentException("Last Argument must be a tail context.");
            }

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
                        result = Sequence(env, (p.Cdr as Pair));
                });
                return result;
            }

            [Primitive("$let", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, "IsCyclic")]
            [PredicateAssertion(1, "ValidBindingList")]
            public static Object Let(Environment environment, List bindings, List objects)
            {
                HashSet<Object> bindingSet = new HashSet<Object>();
                if (bindings.Select<Pair>(binding => binding.Car).All<Object>(bindingSet.Add))
                {
                    throw new ArgumentException("Duplicate bindings.");
                }
                Environment child = new Environment(environment);
                bindings.ForEach<Object>(binding =>
                {
                    Object expression = CarFamily.Cadr(binding as Pair);
                    Match(child, (binding as Pair).Car, environment.Evaluate(expression));
                });
                if (objects.Count() == 0) return Inert.Instance;
                return objects.Select<Object>(environment.Evaluate).Last();
            }

            [Primitive("$let*", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, "IsCyclic")]
            [PredicateAssertion(1, "ValidBindingList")]
            public static Object LetStar(Environment environment, List bindings, List objects)
            => Let(environment, bindings, objects);

            [Primitive("$letrec", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, "IsCyclic")]
            [PredicateAssertion(1, "ValidBindingList")]
            public static Object LetRec(Environment environment, List bindings, List objects)
            {
                HashSet<Object> bindingSet = new HashSet<Object>();
                if (bindings.Select<Pair>(binding => binding.Car).All<Object>(bindingSet.Add))
                    throw new ArgumentException("Duplicate bindings.");
                Environment child = new Environment(environment);
                bindings.ForEach<Object>(binding =>
                {
                    Object expression = CarFamily.Cadr(binding as Pair);
                    Match(child, (binding as Pair).Car, child.Evaluate(expression));
                });
                if (objects.Count() == 0) return Inert.Instance;
                return objects.Select<Object>(environment.Evaluate).Last();
            }

            [Primitive("$letrec*", 2, true)]
            [TypeAssertion(0, typeof(Environment))]
            [TypeAssertion(1, typeof(List))]
            [PredicateAssertion(1, "IsCyclic")]
            [PredicateAssertion(1, "ValidBindingList")]
            public static Object LetRecStar(Environment environment, List bindings, List objects)
            => LetRec(environment, bindings, objects);

            [Primitive("$and?", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object And(Environment environment, List objects)
            {
                for (int i = 0; i < objects.Count() - 1; i++)
                {
                    var result = environment.Evaluate(objects[i]);
                    if (!(result is Boolean))
                        throw new ArgumentException("Argument does not evaluate to a boolean");
                    if (result == Boolean.False)
                        return Boolean.False;
                }
                return environment.Evaluate(objects.Last());
            }

            [Primitive("$or?", 1, true)]
            [TypeAssertion(0, typeof(Environment))]
            public static Object Or(Environment environment, List objects)
            {
                for (int i = 0; i < objects.Count() - 1; i++)
                {
                    var result = environment.Evaluate(objects[i]);
                    if (!(result is Boolean))
                        throw new ArgumentException("Argument does not evaluate to a boolean");
                    if (result == Boolean.True)
                        return Boolean.True;
                }
                return environment.Evaluate(objects.Last());
            }

        }

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
                     .Where(t => t.IsSubclassOf(typeof(Object)) && !t.IsGenericType))
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
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                var pipedMethod = CreatePipeFunction(method);
                Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
                functions.Add(primitiveInformation.PrimitiveName, app);
            }
        }
    }
}