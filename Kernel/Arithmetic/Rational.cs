using System.Numerics;
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
        public readonly BigInteger numerator;

        /// <summary>
        /// The denominator.
        /// </summary>
        public readonly BigInteger denominator;

        public Rational(BigInteger numerator)
        {
            this.numerator = numerator;
            denominator = BigInteger.One;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kernel.Arithmetic.Rational"/> class.
        /// </summary>
        /// <param name="numerator">Numerator.</param>
        /// <param name="denominator">Denominator.</param>
        public Rational(BigInteger numerator, BigInteger denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.</returns>
        public override string ToString() => $"{numerator}/{denominator}";
    }
}
