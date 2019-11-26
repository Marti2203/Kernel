using System.Diagnostics;
using static Kernel.Primitives.Primitives;
using static Kernel.Primitives.Operatives;
using Kernel.BaseTypes;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using Kernel.Utilities;
using System.Linq;
using System.Reflection;
namespace Kernel.Combiners
{
    [DebuggerDisplay("{Name}")]
    public sealed class Operative : Combiner
    {
        readonly IOperative underlyingOperative;

        public Operative(System.Func<List, Environment, Object> operation, string name = "Undefined")
            : base(name)
        {
            underlyingOperative = new PrimitiveOperative(operation);
        }

        // This is Used only by the Add Operatives Method
        internal Operative(System.Func<List, Object> operation, string name = "Undefined")
            : this((@object, environment) => operation(new Pair(environment, @object)), name)
        {

        }

        public Operative(Environment @static, Object formals, Object envformal, List exprs)
            : base("User Defined Operative")
        {
            underlyingOperative = new SyntheticOperative(@static, formals, envformal, exprs);
        }

        interface IOperative
        {
            Object Action(List list, Environment environment);
        }

        #region Underlying Operatives

        class SyntheticOperative : IOperative
        {
            public readonly Environment @static;
            public readonly Object formals;
            public readonly Object envformal;
            public readonly List exprs;
            private readonly Environment local;

            public SyntheticOperative(Environment @static, Object formals, Object envFormal, List exprs)
            {
                this.@static = @static;
                this.formals = formals;
                this.envformal = envFormal;
                this.exprs = exprs;
                local = new Environment(@static);
            }
            public Object Action(List list, Environment dynamicEnvironment)
            {
                Match(local, formals, list);
                if (envformal is Symbol s)
                    local[s] = dynamicEnvironment;
                return Sequence(local, exprs);
            }
        }

        class PrimitiveOperative : IOperative
        {
            readonly System.Func<List, Environment, Object> operation;
            public PrimitiveOperative(System.Func<List, Environment, Object> operation)
            {
                this.operation = operation;
            }

            public Object Action(List list, Environment environment)
            {
                return operation(list, environment);
            }
        }

        private bool IsPrimitive => this.underlyingOperative is PrimitiveOperative;

        #endregion

        public override Object Invoke(List list)
        {
            if (!(list is Pair p))
                throw new System.ArgumentException("Argument is not a Pair");
            if (!(p.Car is List l))
                throw new System.ArgumentException("Argument Car is not a List");
            if (!(p.Cdr is Environment environment))
                throw new System.ArgumentException("Argument Cdr is not an Environment");

            return underlyingOperative.Action(l, environment);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Object Invoke(List list, Environment environment) => underlyingOperative.Action(list, environment);

        public override string ToString() => Name;

        public override bool Equals(Object other)
        => (ReferenceEquals(this, other))
        || (other is Operative operative && underlyingOperative == operative.underlyingOperative);


        public static Operative Optimize(Operative op, Symbol name, Environment e)
        {
            if (op.IsPrimitive)
            {
                return op; // Already optimized or generated
            }
            var underlying = op.underlyingOperative as SyntheticOperative;

            ParameterExpression[] parameters = new[] { Parameter(typeof(List), "input"), Parameter(typeof(Environment), "env") };

            
            LabelTarget startLabelTarget = Label("Start");
            Expression Transform(Object o)
            {
                ConstantExpression staticEnvironmentConstant = Constant(underlying.@static);

                if (o is Symbol)
                    return Call(null, Evaluate, Constant(o), staticEnvironmentConstant);

                if (!(o is Pair pairs))
                    return Constant(o);

                if (!(pairs.Car is Symbol s))
                    throw new System.InvalidOperationException("First element of pair has to be symbol.");

                if (s == name)
                {
                    var arguments = pairs.Skip(1);
                    var gotoStart = Goto(startLabelTarget);

                    var assignments = System.Array.Empty<Expression>();
                    var argumentsCount = arguments.Count();
                    if (argumentsCount == 1 && underlying.formals is Symbol assignmentSymbol)
                    {
                        assignments = new Expression[] { Assign(Property(staticEnvironmentConstant,"Item", Constant(assignmentSymbol))
                                                            ,Call(staticEnvironmentConstant,EvaluateInstance, Constant(arguments[0]))) };
                    }
                    else if (underlying.formals is List l && argumentsCount == l.Count())
                    {
                        if (l.Any<Object>(x => !(x is Symbol)))
                        {
                            throw new System.InvalidOperationException("WTF");
                        }
                        assignments = l.Select((System.Func<Object, int, Expression>)((symbol, i) => Assign(Property(staticEnvironmentConstant, "Item", Constant(l[i]))
                                                                                        , Call(instance: staticEnvironmentConstant, method: EvaluateInstance, Constant(arguments[i])))))
                                        .ToArray();
                    }
                    else
                    {
                        throw new System.InvalidOperationException("WTF");
                    }
                    var recurseBlock = Block(typeof(Object), assignments.Concat(new Expression[] { gotoStart, Constant(null, typeof(Object)) }));

                    return recurseBlock;
                }

                switch (s)
                {
                    case "$if":
                        return pairs.Count() == 4 ? Condition(Convert(TypeAs(Transform(pairs[1]), typeof(Boolean)), typeof(bool)),
                                                               Transform(pairs[2]),
                                                               Transform(pairs[3]), typeof(Object))
                                                  : IfThen(Convert(TypeAs(Transform(pairs[1]), typeof(Boolean)), typeof(bool)),
                                                               Transform(pairs[2]));
                    case "$sequence":
                        return Block(typeof(Object),pairs.Select<Object,Expression>(Transform));
                    case "$define!":
                        Define(underlying.@static, pairs[1], pairs.Skip(2)[0]);
                        return Empty();
                    default:
                        if (!(e[s] is Combiner c))
                            throw new System.InvalidOperationException("Cannot compile a pair that doesn't have a combiner as a first argument");
                        var invoke = c.GetType().GetMethod("Invoke", new[] { typeof(List) });
                        var arguments = MethodCallUtilities.CallFunction("Skip", Constant(pairs), Constant(1));
                        if(c is Applicative)
                        {
                            arguments = Call(arguments, typeof(List).GetMethod("EvaluateAll"), staticEnvironmentConstant);
                        }
                        return Call(Constant(c),invoke,arguments);

                }
            }

            var bodyStatements = GenerateParameterSettings(underlying, parameters[0])
                                .Concat(new Expression[] { Label(startLabelTarget) })
                                .Concat(underlying.exprs.Select<Object, Expression>(Transform))
                                .ToArray();

            var last = bodyStatements.Last();

            BlockExpression body = Block(typeof(Object), bodyStatements);

            System.Console.WriteLine($"Body for {name} is : ");
            Display(body);
            return new Operative(Lambda<System.Func<List, Environment, Object>>(body, name, true, parameters).Compile(), name);
        }

        private static Expression[] GenerateParameterSettings(SyntheticOperative underlying, ParameterExpression input)
        {
            var l = underlying.formals as List;
            if (l.Any<Object>(x => x is Pair p))
            {
                throw new System.NotSupportedException("Currently Kernel does not support optimization of operatives which have a pair as an argument.");
            }
            return l.Select<Object, Expression>((x, i) => x switch
            {
                Symbol s => Assign(Property(Constant(underlying.@static), "Item", Constant(s)),
                            Property(input, "Item", Constant(i))),
                Ignore _ => Empty(),
                Null _ => throw new System.InvalidOperationException("Got null argument when generating parameters, why?"),
                _ => throw new System.InvalidOperationException($"Got {x.GetType()} when generating parameters, this is not a valid binding list"),
            }).ToArray();
        }

        private static void Display(Expression s, string format = "{0}")
        {
            if (s is BlockExpression block)
            {
                System.Console.WriteLine(format.Replace("{0}", string.Empty));
                foreach (var e in block.Expressions)
                    Display(e);
            }
            else if (s is ConditionalExpression cond)
            {
                Display(cond.Test, "Test: {0}");
                Display(cond.IfTrue, "IfTrue: {0}");
                Display(cond.IfFalse, "IfFalse: {0}");
            }
            else
                System.Console.WriteLine(format, s);
        }
        private static readonly MethodInfo Evaluate = typeof(Primitives.Primitives)
                                            .GetMethod("Evaluate", new[] { typeof(Object), typeof(Environment) });
        private static readonly MethodInfo EvaluateInstance = typeof(Environment)
                                    .GetMethod("Evaluate", new[] { typeof(Object) });

    }
}
