using System;
using static Kernel.Primitives.Primitives;
namespace Kernel.Combiners
{
    public sealed class Operative : Combiner
    {
        readonly IOperative underlyingOperative;

        public Operative(Func<Environment, Object[], Object> operation, int inputCount, bool variadic = false)
            : base(inputCount, variadic)
        {
            underlyingOperative = new PrimitiveOperative(operation);
        }

        public Operative(Environment env, Object formals, Object eformal, Object expr)
            :base(-1,true)
        {
            underlyingOperative = new CompoundOperative(env, formals, eformal, expr);
        }

        interface IOperative { Object Action(Object[] objects); }

        class CompoundOperative : IOperative
        {
            readonly Environment @static;
            readonly Object formals;
            readonly Object eformal;
            readonly Object expr;

            public CompoundOperative(Environment @static, Object formals, Object eformal, Object expr)
            {
                this.@static = @static;
                this.formals = formals;
                this.eformal = eformal;
                this.expr = expr;
            }
            public Object Action(Object[] objects)
            {
                if (!(objects[1] is Environment env))
                    throw new ArgumentException("Second argument must be an environment");

                Environment local = new Environment(@static);
                Match(local, formals, objects[0]);
                if (eformal is Symbol s)
                    local[s] = objects[1];
                if(IsTailContext(expr))
                return local.Evaluate(expr);
                throw new InvalidOperationException("WTF?!");
            }
        }

        class PrimitiveOperative : IOperative
        {
            readonly Func<Environment, Object[], Object> operation;
            public PrimitiveOperative(Func<Environment, Object[], Object> operation)
            {
                this.operation = operation;
            }

            public Object Action(Object[] objects) => operation(Environment.Current, objects);
        }

        protected override Object Action(Object[] objects) => underlyingOperative.Action(objects);

        public bool Equals(Operative other) => underlyingOperative == other.underlyingOperative;
    }
}
