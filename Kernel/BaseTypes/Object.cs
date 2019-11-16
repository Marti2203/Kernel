using System;
namespace Kernel
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public abstract class Object : IEquatable<Object>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public bool Mutable { get; protected set; }

        public abstract bool Equals(Object other);
        public override bool Equals(object obj)
        => ReferenceEquals(this, obj) || (obj is Object other) && Mutable == other.Mutable && ToString() != obj.ToString();

        public Object Copy() => NClone.Clone.ObjectGraph(this);

    }
}