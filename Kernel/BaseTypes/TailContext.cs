using System;
namespace Kernel
{
    public class TailContext : Object
    {
        Pair data;
        public TailContext(Combiners.Combiner combiner, Object list)
        {
            data = new Pair
            {
                Car = combiner,
                Cdr = list
            };
        }

        public override Object Evaluate(params Object[] input)
        {
            return data.Evaluate(input);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
