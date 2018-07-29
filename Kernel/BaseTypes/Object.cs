using System;
namespace Kernel
{
    public abstract class Object : IEquatable<Object>
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Kernel.Object"/> is mutable.
        /// </summary>
        /// <value><c>true</c> if mutable; otherwise, <c>false</c>.</value>
        public bool Mutable { get; protected set; }

        /// <summary>
        /// Determines whether the specified <see cref="Kernel.Object"/> is equal to the current <see cref="T:Kernel.Object"/>.
        /// </summary>
        /// <param name="other">The <see cref="Kernel.Object"/> to compare with the current <see cref="T:Kernel.Object"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Kernel.Object"/> is equal to the current <see cref="T:Kernel.Object"/>;
        /// otherwise, <c>false</c>.</returns>
        // Todo : Not Fully complete
#warning Equals(eq?) may create issues
        public virtual bool Equals(Object other)
        {
            if (Mutable != other.Mutable) return false;
            if (!ReferenceEquals(this, other)) return false;
            if (GetType() != other.GetType()) return false;
            if (ToString() != other.ToString()) return false;
            return true;
        }

        public Object Copy() => DeepCopy.DeepCopier.Copy(this);
    }
}