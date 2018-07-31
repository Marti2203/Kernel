using System;
namespace Kernel.Combiners
{
	public sealed class Applicative : Combiner
	{
		public Applicative(Func<Object, Object> application, string name = "Undefined")
			: base(name)
		{
			combiner = new Operative((@object, env) =>
			{
				return application(@object);
			}, name);
		}

		public Applicative(Combiner combiner)
		{
			this.combiner = combiner;
			Mutable = combiner.Mutable;
		}

		public readonly Combiner combiner;

		public override Object Invoke(Object @object)
		{
			if (combiner is Applicative)
				return combiner.Invoke(@object);
			if (combiner is Operative)
				return combiner.Invoke(new Pair(@object, Environment.Current));
			throw new InvalidOperationException("A different combiner?!");
		}
		public bool Equals(Applicative other) => combiner == other.combiner;

		public override string ToString() => Name ?? combiner.Name;
	}

}
