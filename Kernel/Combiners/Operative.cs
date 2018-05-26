using System;
namespace Kernel.Combiners
{
    public sealed class Operative : Combiner
    {
        Func<Environment, Object[], Object> operation;
        readonly int inputCount;
        readonly bool variadic;

        readonly bool compound;
        readonly Environment @static;
        readonly Object formals;
        readonly Object eformal;
        readonly Object expr;

        public override int InputCount => inputCount;

        public Operative(Func<Environment, Object[], Object> operation, int inputCount, bool variadic = false)
        {
			compound = false;
            this.operation = operation;
            this.inputCount = inputCount;
            this.variadic = variadic;
        }

        public Operative(Environment env,Object formals,Object eformal,Object expr)
        {
            compound = true;
            @static = env;
            this.formals = formals;
            this.eformal = eformal;
            this.expr = expr;
        }


        public override bool Evaluated => false;

        public override Object Evaluate(params Object[] input)
        {
            if (!variadic) base.Evaluate(input);
            else if (input.Length < inputCount) throw new InvalidOperationException("Not enough arguments for operative");

            if (compound)
            {
                if (!(input[1] is Environment env))
                    throw new ArgumentException("Second argument must be an environment");

                Environment local = new Environment(@static);
                Primitives.Match(local,formals,input[0]);
                if (eformal is Symbol s)
                    local[s] = input[1];
                return local.Evaluate(expr as TailContext);
            }
            return operation(Environment.Current, input);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        //Todo implementation dependent, may not be good
        public bool Equals(Operative other) => operation != other.operation;
    }
}
