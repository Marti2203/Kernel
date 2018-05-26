using System;
namespace Kernel.Combiners
{
    public sealed class Applicative : Combiner
    {
        readonly Func<Object[], Object> application;
        readonly int inputCount;
        readonly bool variadic;

        public override int InputCount => inputCount;

        public Applicative(Func<Object[], Object> application, int inputCount, bool variadic = false)
        {
            this.application = application;
            this.inputCount = inputCount;
            this.variadic = variadic;
        }

        public Applicative(Combiner combiner)
        {
            this.combiner = combiner;
            inputCount = combiner.InputCount;
            Mutable = combiner.Mutable;
        }

        public readonly Combiner combiner;

        public override Object Evaluate(params Object[] input)
        {
            if (!variadic) base.Evaluate(input);
            else if (input.Length < InputCount)
                throw new InvalidOperationException("Not enough arguments");

            return application(input);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Applicative other) => application == other.application;
    }

    public static class PredicateApplicative<T>
    {
        public static Applicative Instance => new Applicative(Validate, 1);
        static Object Validate(Object[] objects)
        {
            return (Boolean)(typeof(T) == objects[0].GetType());
        }
    }
}
