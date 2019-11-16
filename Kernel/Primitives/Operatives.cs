#define FastCopyES
using System;
using Kernel.Combiners;
using static CarFamily;
using Kernel.Utilities;
using System.Linq;
using static Kernel.Primitives.Primitives;
using Kernel.BaseTypes;

namespace Kernel.Primitives
{
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
            Object @object = env.Evaluate(exp2);
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
}