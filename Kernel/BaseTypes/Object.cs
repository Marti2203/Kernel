using System;
namespace Kernel
{
    public abstract class Object : IEquatable<Object>
    {
        /// <summary>
        /// Evaluate the specified input.
        /// </summary>
        /// <returns>The evaluate.</returns>
        /// <param name="input">Input.</param>
        public virtual Object Evaluate(params Object[] input)
        {
            if (InputCount != input.Length)
                throw new InvalidOperationException("Unequal required and given argument lengths");

            return this;
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Kernel.Object"/> is mutable.
        /// </summary>
        /// <value><c>true</c> if mutable; otherwise, <c>false</c>.</value>
        public bool Mutable { get; protected set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Kernel.Object"/> is evaluated.
        /// </summary>
        /// <value><c>true</c> if evaluated; otherwise, <c>false</c>.</value>
        public virtual bool Evaluated => true;
        /// <summary>
        /// Gets the input count.
        /// </summary>
        /// <value>The input count.</value>
        public virtual int InputCount => 0;
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Object"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Object"/>.</returns>
        new public abstract string ToString();

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