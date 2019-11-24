#define FastCopyES
using System;
using System.Collections.Generic;
using System.Reflection;
using Kernel.Combiners;
using static Kernel.Primitives.DynamicBinding.DynamicFunctionBinding;
using static CarFamily;
using Kernel.Utilities;
using System.Linq;
using Kernel.BaseTypes;
using Object = Kernel.BaseTypes.Object;
using Environment = Kernel.BaseTypes.Environment;
using Kernel.Primitives.DynamicBinding.Attributes;

namespace Kernel.Primitives
{
    public static partial class Primitives
    {
        public static Combiner Get(string name)
        => functions.ContainsKey(name) ? functions[name] : throw new NoBindingException("No such primitive");
        public static bool Has(string name)
        => functions.ContainsKey(name);

        static readonly IDictionary<string, Combiner> functions = new Dictionary<string, Combiner>();

        static Primitives()
        {
            AddPredicateApplicatives();
            AddApplicatives();
            AddCarFamily();
            AddOperatives();
        }

        public static bool IsInputPort(Object obj) => obj is Port p && p.Type == PortType.Input;
        public static bool IsOutputPort(Object obj) => obj is Port p && p.Type == PortType.Output;

        public static bool ValidBindingList(Object obj)
        => obj is List l && l.All<Object>(element => element is Pair pair && IsFormalParameterTree(pair.Car) && !(pair.Cdr is Null));

        public static bool UniqueBindingList(Object obj)
        => obj is List bindings && bindings.Select<Pair>(Car<Object>).All<Object>(new HashSet<Object>().Add);

        public static bool AllPairs(Object obj) => obj is Pair p && p.All<Object>(@object => @object is Pair);

        public static bool AllSymbols(Object obj) => obj is Pair p && p.All<Object>(@object => @object is Symbol);

        public static bool ContainsCycle(Object obj)
        => obj is List l && l.IsCyclic;

        public static Object Evaluate(Object @object, Environment environment)
        {
            Object result;
            if (environment != Environment.Current)
            {
                Environment temp = Environment.Current;
                Environment.Current = environment;
                result = environment.Evaluate(@object);
                Environment.Current = temp;
            }
            else
            {
                result = Environment.Current.Evaluate(@object);
            }

            return result;
        }
        public static Object Evaluate(Combiner c, List l, Environment environment)
        {
            Environment temp = Environment.Current;
            Environment.Current = environment;
            Object result = environment.Evaluate(c, l);
            Environment.Current = temp;
            return result;
        }

        public static bool IsFormalParameterTree(Object @object) => IsFormalParameterTree(@object, true);

        public static void Match(Environment env, Object definiend, Object result)
        {
            Stack<(Object definiend, Object result)> toMatch = new Stack<(Object, Object)>();
            toMatch.Push((definiend, result));

            List<Action> settingOperations = new List<Action>();

            List<ArgumentException> errors = new List<ArgumentException>(1);

            while (toMatch.Count != 0 && errors.Count == 0)
            {
                var (definiendCurrent, resultCurrent) = toMatch.Pop();

                switch (definiendCurrent)
                {
                    case Symbol s:
                        settingOperations.Add(resultCurrent is Combiner c ? new Action(() => OptmizeCombinerMatch(env, s, c))
                                                                          : () => env[s] = resultCurrent);
                        break;
                    case Null _:
                        if (!(resultCurrent is Null))
                        {
                            errors.Add(new ArgumentException($"Cannot match expression {resultCurrent} to null"));
                        }
                        break;
                    case Pair pDefiniend:
                        if (resultCurrent is Pair pExpression)
                        {
                            toMatch.Push((pDefiniend.Car, pExpression.Car));
                            toMatch.Push((pDefiniend.Cdr, pExpression.Cdr));
                        }
                        else
                            errors.Add(new ArgumentException($"Cannot match expression {resultCurrent} to pair"));
                        break;
                    default:
                        throw new InvalidOperationException("WATAFAK");
                }
            }
            if (errors.Count == 0)
            {
                foreach (var action in settingOperations)
                    action();
            }
            else throw new AggregateException(errors);
        }

        private static void OptmizeCombinerMatch(Environment e, Symbol s, Combiner c)
        {
            if (c is Applicative a)
            {
                while (a.Combiner is Applicative aInner)
                {
                    a = aInner;
                }
                if (!(a.Combiner is Operative op))
                    throw new InvalidOperationException("WTF??");
                e[s] = new Applicative(Operative.Optimize(op,s,e));
            }
            else if (c is Operative o)
            {
                e[s] = Operative.Optimize(o,s,e);
            }
            else
            {
                throw new InvalidOperationException("WTF??");
            }
        }

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

        #region Primitive Generation

        static void AddApplicatives()
        {
            foreach (MethodInfo method in typeof(Applicatives)
                     .GetMethods()
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                Applicative app = new Applicative(CreateBinding(method), primitiveInformation.PrimitiveName);
                if (functions.ContainsKey(primitiveInformation.PrimitiveName))
                {
                    functions[primitiveInformation.PrimitiveName] = app;
                }
                else
                {
                    functions.Add(primitiveInformation.PrimitiveName, app);

                }
            }
        }

        static void AddOperatives()
        {
            foreach (MethodInfo method in typeof(Operatives)
                     .GetMethods()
                     .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
            {
                PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
                Operative app = new Operative(CreateBinding(method), primitiveInformation.PrimitiveName);
                if (functions.ContainsKey(primitiveInformation.PrimitiveName))
                {
                    functions[primitiveInformation.PrimitiveName] = app;
                }
                else
                {
                    functions.Add(primitiveInformation.PrimitiveName, app);

                }
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
                var pipedMethod = CreateBinding(method);
                Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
                functions.Add(primitiveInformation.PrimitiveName, app);
            }
        }
        #endregion
    }
}