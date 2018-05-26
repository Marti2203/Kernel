using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Rational number, consisting of a numerator and denominator.
    /// </summary>
    public sealed class Rational : Number
    {
        /// <summary>
        /// The numerator.
        /// </summary>
        public readonly long numerator;

        /// <summary>
        /// The denominator.
        /// </summary>
        public readonly long denominator;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kernel.Arithmetic.Rational"/> class.
        /// </summary>
        /// <param name="numerator">Numerator.</param>
        /// <param name="denominator">Denominator.</param>
        public Rational(long numerator, long denominator = 1)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.</returns>
        public override string ToString() => $"{numerator}/{denominator}";

        public static implicit operator Real(Rational rational) 
        => new Real((rational.numerator * 1.0M) / rational.denominator);
    }
}
