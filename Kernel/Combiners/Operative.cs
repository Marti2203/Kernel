using System.Diagnostics;
using static Kernel.Primitives.Primitives;
using static Kernel.Primitives.Operatives;
using Kernel.BaseTypes;
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

        public Operative(Environment @static, Object formals, Object eformal, List exprs)
            : base("User Defined Operative")
        {
            underlyingOperative = new CompoundOperative(@static, formals, eformal, exprs);
        }

        interface IOperative
        {
            Object Action(List list, Environment environment);
        }

        #region Underlying Operatives

        class CompoundOperative : IOperative
        {
            readonly Environment @static;
            readonly Object formals;
            readonly Object eformal;
            readonly List exprs;

            public CompoundOperative(Environment @static, Object formals, Object eformal, List exprs)
            {
                this.@static = @static;
                this.formals = formals;
                this.eformal = eformal;
                this.exprs = exprs;
            }
            public Object Action(List list, Environment dynamicEnvironment)
            {
                Environment local = new Environment(@static);
                Match(local, formals, list);
                if (eformal is Symbol s)
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

        #endregion

        public override Object Invoke(List list)
        {
            string exceptionMessage = "Argument is not a Pair";
            if (list is Pair p)
            {
                if (p.Car is List l && p.Cdr is Environment environment)
                    return underlyingOperative.Action(l, environment);
                if (!(p.Car is List))
                    exceptionMessage += " and Argument Car is not a List";
                if (!(p.Cdr is Environment))
                    exceptionMessage += " and Argument Cdr is not a List";
            }
            throw new System.ArgumentException(exceptionMessage);
        }

        public Object Invoke(List list, Environment environment) => underlyingOperative.Action(list, environment);

        public override string ToString() => Name;

        public override bool Equals(Object other)
        => (ReferenceEquals(this, other))
        || (other is Operative operative && underlyingOperative == operative.underlyingOperative);
    }
}
