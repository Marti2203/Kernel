namespace Kernel
{
    public sealed class Promise : Object
    {
        public Object Result => evaluated ? result : (result = Evaluate());
        Object result;
        bool evaluated;
        readonly Object expression;
        readonly Environment environment;
        public Promise(Environment environment, Object expression)
        {
            this.expression = expression;
            this.environment = environment;
        }
        public Object Evaluate()
        {
            Object value = Primitives.Primitives.Evaluate(expression, environment);
            if (!evaluated)
            {
                result = value;
                evaluated = true;
            }
            return result;
        }
        public override bool Equals(Object other) => ReferenceEquals(this, other);
        public override string ToString() => evaluated ? $"Promise<{result}>" : "Promise<not forced>";
    }
}
