using System;
using System.Linq;
namespace Kernel.Combiners
{
	public abstract class Combiner : Object
	{
		public int InputCount => inputCount;

		readonly int inputCount;

		protected readonly bool variadic;
		protected abstract Object Action (Pair objects);
		public Object Invoke (Pair objects)
		{
			int size = objects.Count ();
			if (size < InputCount || (!variadic && size != InputCount))
				throw new InvalidOperationException ("Not enough arguments for combiner");
			return Action (objects);
		}
		public Object Invoke () => Invoke (new Pair ());
		protected Combiner (int inputCount = 0, bool variadic = false)
		{
			this.inputCount = inputCount;
			this.variadic = variadic;
		}
	}
}
