using System;
namespace Kernel
{
    public abstract class Object : IEquatable<Object>
    {
        public bool Mutable { get; protected set; }

        public abstract bool Equals(Object other);
        public override bool Equals(object obj)
        => ReferenceEquals(this, obj) || (obj is Object other) && Mutable == other.Mutable && ToString() != obj.ToString();

        public Object Copy() => NClone.Clone.ObjectGraph(this);

    }
}