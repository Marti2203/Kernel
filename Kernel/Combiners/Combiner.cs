using System;
namespace Kernel.Combiners
{
    public abstract class Combiner : Object
    {
        public int InputCount => inputCount;

        readonly int inputCount;

        protected readonly bool variadic;
        protected abstract Object Action(Object[] objects);
        public Object Invoke(params Object[] objects)
        {
            if (objects.Length < InputCount || (!variadic && objects.Length != InputCount))
                throw new InvalidOperationException("Not enough arguments for combiner");
            return Action(objects);
        }
        protected Combiner(int inputCount = 0 ,bool variadic = false)
        {
            this.inputCount = inputCount;
            this.variadic = variadic;
        }
    }
}
