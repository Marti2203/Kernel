using System.Diagnostics;
namespace Kernel.Combiners
{
	[DebuggerDisplay("{Name}")]
	public abstract class Combiner : Object
	{
		public string Name => name;
		readonly string name;

		public abstract Object Invoke(Object @object);

		public Object Invoke() => Invoke(Null.Instance);
		protected Combiner(string name = "Undefined")
		{
			this.name = name;
		}
	}
}
