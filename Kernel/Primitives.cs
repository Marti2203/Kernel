#define FastCopyEs
using System;
using System.Collections.Generic;
using System.Linq;
using Kernel.Combiners;
namespace Kernel
{
    public static class Primitives
    {
        static readonly bool Variadic = true;
        public static Object Get(string name)
        => data.ContainsKey(name) ? data[name] : throw new NoBindingException("No such primitive");
        public static bool Has(string name)
        => data.ContainsKey(name);

        static readonly IDictionary<string, Object> data = new Dictionary<string, Object>
        {
            //Todo Cadddr famiy
            {"read", new Applicative(Applicatives.Read,0)},
            {"apply",null},
            {"load",null},
            {"write",null},

            {"eq?",new Applicative(objects=>
                objects.Length == 0 || objects.All(obj=>obj.Equals(objects[0])) ? Boolean.True : Boolean.False
            ,0,Variadic)},
            {"equal?", new Applicative(Applicatives.Equal,0,Variadic)},

            {"#t", Boolean.True},
            {"#f", Boolean.False},
            {"()", Null.Instance},
            {"#inert", Inert.Instance},
            {"#ignore", Ignore.Instance},


            {"null?", PredicateApplicative<Null>.Instance},
            {"inert?", PredicateApplicative<Inert>.Instance},
            {"boolean?", PredicateApplicative<Boolean>.Instance},
            {"symbol?", PredicateApplicative<Symbol>.Instance},
            {"pair?", PredicateApplicative<Pair>.Instance},
            {"environment?" , PredicateApplicative<Environment>.Instance},
            {"ignore?", PredicateApplicative<Ignore>.Instance},
            {"operative?", PredicateApplicative<Operative>.Instance},
            {"applicative?", PredicateApplicative<Applicative>.Instance},

            {"eval", new Applicative(Applicatives.Eval,2)},

            {"make-environment", new Applicative(Applicatives.MakeEnvironment,0,Variadic)},

            {"cons", new Operative((env,objects) => new Pair{Car = objects[0], Cdr = objects[1]},2)},
            {"car", new Applicative(Applicatives.Car,1)},
            {"cdr", new Applicative(Applicatives.Cdr,1)},


            {"copy-es-immutable", new Applicative(Applicatives.CopyEvaluationStructureImmutable,1)},

            {"$define", new Operative(Operatives.Define,2)},
            {"$vau", new Operative(Operatives.Vau,3,Variadic)},

            {"wrap", new Applicative(Applicatives.Wrap,1)},
            {"unwrap", new Applicative(Applicatives.Unwrap,1)},
            {"$lambda", new Operative(Operatives.Lambda,2)},

            {"set-car!", new Applicative(Applicatives.SetCar,2)},
            {"set-cdr!", new Applicative(Applicatives.SetCdr,2)},

            {"$sequence", new Operative(Operatives.Sequence,0,Variadic)},
            {"$cond", new Operative(Operatives.Cond,0,Variadic)},
            {"apply",new Applicative(Applicatives.Apply,2,Variadic)},


            {"$and?",null},
            {"list", new Applicative(Applicatives.List,1,Variadic)},
            {"list*", new Applicative(Applicatives.ListStar,1,Variadic)},
            {"$if", new Operative(Operatives.If,3)},
        };


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


        static class Applicatives
        {
            public static Object Read(params Object[] objects)
            {
                bool Valid(string representation)
                {
                    int braces = 0;
                    bool inString = false;
                    for (int i = 0; i < representation.Length; i++)
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
                    return braces == 0 && !inString && representation.Trim().Length != 0;
                }


                string input = Console.ReadLine();

                while (!Valid(input))
                    input += Console.ReadLine();

                return Parser.Parse(input.Trim());
            }


            public static Object SetCar(params Object[] objects)
            {
                if (!(objects[0] is Pair p))
                    throw new ArgumentException("First argument must be a pair");
                if (!p.Mutable)
                    throw new ArgumentException("Cannot mutate immutable pair");
                p.Car = objects[1];
                return Inert.Instance;
            }

            public static Object SetCdr(params Object[] objects)
            {
                if (!(objects[0] is Pair p))
                    throw new ArgumentException("First argument must be a pair");
                if (!p.Mutable)
                    throw new ArgumentException("Cannot mutate immutable pair");
                p.Cdr = objects[1];
                return Inert.Instance;
            }


            // Todo Cyclic pairs?
            public static Object CopyEvaluationStructureImmutable(params Object[] objects)
            {

                if (!(objects[0] is Pair p)) return objects[1];

                // HACK Why bother with creating a new one?
#if FastCopyES
                if (!p.Mutable) return p; 
#endif
                return new Pair(CopyEvaluationStructureImmutable(p.Car),
                                CopyEvaluationStructureImmutable(p.Cdr), false);
            }

            public static Object Eval(params Object[] objects)
            {
                if (!(objects[0] is TailContext t))
                    throw new ArgumentException("First Object must be a tail context");
                if (!(objects[1] is Environment env))
                    throw new ArgumentException("Second object must be an environment");
                return env.Evaluate(t);
            }

            public static Object MakeEnvironment(params Object[] objects)
            {
                if (!objects.All(e => e is Environment))
                    throw new ArgumentException("Non environment object is environment list");
                return new Environment(objects.Cast<Environment>());
            }

            public static Object Equal(params Object[] objects)
            {
                if (objects.Length == 0) return Boolean.True;
                if (objects.All(obj => obj.GetType() == objects[0].GetType() && obj.Equals(objects[0]))) return Boolean.True;
                return Boolean.False;
            }

            public static Object Wrap(params Object[] objects)
            => objects[0] is Combiner c ? new Applicative(c) :
                    throw new ArgumentException("First argument must be a combiner");

            public static Object Unwrap(params Object[] objects)
            => objects[0] is Applicative app ? app.combiner :
                    throw new ArgumentException("First argument must be an applicative");

            public static Object List(params Object[] objects)
            {
                if (objects.Length == 1) return objects[0];
                return new List(objects);
            }

            public static Object ListStar(params Object[] objects)
            {
                if (objects.Length == 1) return objects[0];
                List result = new List();
                for (int i = 0; i < objects.Length - 1; i++)
                    result.Append(objects[i]);
                result.Last = objects[objects.Length - 1];
                return result;
            }


            public static Object Car(params Object[] objects)
            {
                if (!(objects[0] is Pair p))
                    throw new ArgumentException("Argument must be a pair");
                return p.Car;
            }

            public static Object Cdr(params Object[] objects)
            {
                if (!(objects[0] is Pair p))
                    throw new ArgumentException("Argument must be a pair");
                return p.Cdr;
            }

            public static Object Apply(params Object[] objects)
            {
                if (!(objects[0] is Applicative a))
                    throw new ArgumentException("First Argument must be an applicative");
                return (objects.Length == 3 ? objects[2] : MakeEnvironment())
                   .Evaluate(new TailContext(a.combiner, objects[1]));
            }
        }

        static class Operatives
        {
            public static Object Lambda(Environment env, params Object[] objects)
            => Applicatives.Wrap(Vau(Environment.Current, new Object[] { objects[0], Ignore.Instance }.Concat(objects.Skip(1)).ToArray()));

            public static Object If(Environment env, params Object[] objects)
            {
                Object test = env.Evaluate(objects[0]);
                if (!(test is Boolean condition)) throw new ArgumentException("Test is not a boolean.");

                if (condition)
                {
                    if (!(objects[1] is TailContext))
                        throw new ArgumentException("Consequent is not a tail context");

                    return env.Evaluate(objects[1]);
                }

                if (!(objects[2] is TailContext))
                    throw new ArgumentException("Alternative is not a tail context");

                return env.Evaluate(objects[2]);
            }

            public static Object Define(Environment env, params Object[] objects)
            {
                if (IsFormalParameterTree(objects[0]))
                    throw new ArgumentException("Definiend must be a formal paramter tree");
                Match(env, objects[0], env.Evaluate(objects[1]));
                return Inert.Instance;
            }

            internal static Object Vau(Environment env, params Object[] objects)
            {
                if (!IsFormalParameterTree(objects[0]))
                    throw new ArgumentException("Formals should be a formal paramter tree");
                if ((objects[0] is Symbol && objects[0] == objects[1])
                   || (objects[0] is Pair p && objects[1] is Symbol && p.Contains(objects[1])))
                    throw new ArgumentException("Formals contain eformal");
                if (!(objects[1] is Symbol) || !(objects[1] is Ignore))
                    throw new ArgumentException("Eformal should either be a symbol or ignore");


                return new Operative(Environment.Current.Copy() as Environment,
                                     Applicatives.CopyEvaluationStructureImmutable(objects[0]),
                                     objects[1],
                                     objects.Length == 3 ?
                                     Applicatives.CopyEvaluationStructureImmutable(objects[2])
                                     : Applicatives.CopyEvaluationStructureImmutable(Sequence(env, objects.Skip(2).ToArray())));

            }

            public static Object Sequence(Environment env, params Object[] objects)
            {
                if (objects.Length == 0) return Inert.Instance;
                for (int i = 0; i < objects.Length - 1; i++)
                    env.Evaluate(objects[i]);
                return env.Evaluate(objects[objects.Length - 1] as TailContext);
            }

            public static Object Cond(Environment env, params Object[] objects)
            {
                if (!(objects.All(o => o is Pair)))
                    throw new ArgumentException("All clauses must be a pair");
                foreach (Pair p in objects.Cast<Pair>())
                {
                    Object test = env.Evaluate(p.Car);
                    if (!(test is Boolean condition)) throw new ArgumentException($"Test in {p.Car} is not a boolean.");

                    if (condition)
                        return Sequence(env,p.Cdr);
                }
                return Inert.Instance;
            }
        }
    }
}