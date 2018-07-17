using System;
namespace Kernel.Combiners
{
	public sealed class Applicative : Combiner
	{
		readonly Func<Pair, Object> application;

		public Applicative (Func<Pair, Object> application, int inputCount, bool variadic = false)
			: base (inputCount, variadic)
		{
			this.application = application;
			this.combiner = new Operative ((Pair pair, Environment env) => application (pair), inputCount, variadic);
		}

		public Applicative (Combiner combiner)
		{
			this.combiner = combiner;
			Mutable = combiner.Mutable;
		}

		public readonly Combiner combiner;

		protected override Object Action (Pair objects) => application.Invoke (objects) as Object;

		public bool Equals (Applicative other) => application == other.application;
	}

	public static class PredicateApplicative<T>
	{
		public static Applicative Instance => new Applicative (Validate, 1);
		static Object Validate (Pair objects)
		{
			return (Boolean)(typeof (T) == objects.Car.GetType ());
		}
	}
}
