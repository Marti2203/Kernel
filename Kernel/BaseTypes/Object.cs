using System;
namespace Kernel
{
	public abstract class Object : IEquatable<Object>
	{
		public bool Mutable { get; protected set; }

		public abstract bool Equals(Object other);
		public override bool Equals(object obj)
		{
			if (!(obj is Object other)) return false;
			if (Mutable != other.Mutable) return false;
			if (ToString() != obj.ToString()) return false;
			return true;
		}

		public Object Copy() => NClone.Clone.ObjectGraph(this);
	}
}