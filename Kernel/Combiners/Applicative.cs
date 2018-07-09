using System;
namespace Kernel.Combiners
{
	public sealed class Applicative : Combiner
	{
		readonly Func<Object [], Object> application;

		public Applicative (Func<Object [], Object> application, int inputCount, bool variadic = false)
			: base (inputCount, variadic)
		{
			this.application = application;
		}

		public Applicative (Combiner combiner)
		{
			this.combiner = combiner;
			Mutable = combiner.Mutable;
		}

		public readonly Combiner combiner;

		protected override Object Action (params Object [] objects)
		{
			return application.Invoke (objects) as Object;
		}
		public bool Equals (Applicative other) => application == other.application;
	}

	public static class PredicateApplicative<T>
	{
		public static Applicative Instance => new Applicative (Validate, 1);
		static Object Validate (Object [] objects)
		{
			return (Boolean)(typeof (T) == objects [0].GetType ());
		}
	}
}
