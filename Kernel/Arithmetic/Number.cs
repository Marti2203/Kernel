using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Number.
    /// </summary>
    public abstract class Number : Object
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Kernel.Arithmetic.Number"/> is exact.
        /// </summary>
        /// <value><c>true</c> if exact; otherwise, <c>false</c>.</value>
        public bool Exact { get; set; }


        /// <summary>
        /// Determines whether the specified <see cref="Kernel.Arithmetic.Number"/> is equal to the current <see cref="T:Kernel.Arithmetic.Number"/>.
        /// </summary>
        /// <param name="other">The <see cref="Kernel.Arithmetic.Number"/> to compare with the current <see cref="T:Kernel.Arithmetic.Number"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Kernel.Arithmetic.Number"/> is equal to the current
        /// <see cref="T:Kernel.Arithmetic.Number"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Number other)
        {
            if (Exact != other.Exact) return false;
            return base.Equals(other as Object);
        }
    }
}
