using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Real.
    /// </summary>
    public sealed class Real : Number
    {
        /// <summary>
        /// The data.
        /// </summary>
        public readonly decimal data;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kernel.Arithmetic.Real"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public Real(decimal value)
        {
            data = value;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Real"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Real"/>.</returns>
        public override string ToString() => data.ToString();

        /// <summary>
        /// Adds a <see cref="Kernel.Arithmetic.Real"/> to a <see cref="Kernel.Arithmetic.Real"/>, yielding a new <see cref="T:Kernel.Arithmetic.Real"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Real"/> to add.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Real"/> to add.</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Real"/> that is the sum of the values of <c>l</c> and <c>r</c>.</returns>
        public static Real operator +(Real l, Real r)
        => new Real(l.data + r.data);

        /// <summary>
        /// Subtracts a <see cref="Kernel.Arithmetic.Real"/> from a <see cref="Kernel.Arithmetic.Real"/>, yielding a new <see cref="T:Kernel.Arithmetic.Real"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Real"/> to subtract from (the minuend).</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Real"/> to subtract (the subtrahend).</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Real"/> that is the <c>l</c> minus <c>r</c>.</returns>
        public static Real operator -(Real l, Real r)
        => new Real(l.data - r.data);

        /// <summary>
        /// Computes the product of <c>l</c> and <c>r</c>, yielding a new <see cref="T:Kernel.Arithmetic.Real"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Real"/> to multiply.</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Real"/> to multiply.</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Real"/> that is the <c>l</c> * <c>r</c>.</returns>
        public static Real operator *(Real l, Real r)
        => new Real(l.data * r.data);

        /// <summary>
        /// Computes the division of <c>l</c> and <c>r</c>, yielding a new <see cref="T:Kernel.Arithmetic.Real"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Real"/> to divide (the divident).</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Real"/> to divide (the divisor).</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Real"/> that is the <c>l</c> / <c>r</c>.</returns>
        public static Real operator /(Real l, Real r)
        => new Real(l.data / r.data);

    }
}
