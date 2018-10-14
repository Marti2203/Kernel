using System.Numerics;
using System.Collections.Generic;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Rational number, consisting of a numerator and denominator.
    /// </summary>
    public sealed class Rational : Number
    {
        public override NumberHierarchy Priority => NumberHierarchy.Rational;

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
        public Rational(string numerator, string denominator)
            : this(BigInteger.Parse(numerator), BigInteger.Parse(denominator))
        {
        }

        public Rational(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
                throw new System.ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero!");
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Rational Get(string numerator, string denominator) => new Rational(numerator, denominator);

        public static Rational Get(BigInteger numerator, BigInteger denominator) => new Rational(numerator, denominator);


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.</returns>
        public override string ToString() => $"{numerator}{ (denominator == 1 ? "" : "/" + denominator) }";

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            Rational rat = (n as Rational);
            return rat.numerator == numerator && rat.denominator == denominator;
        }

        protected override Number Add(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number Subtract(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number SubtractFrom(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number Multiply(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number Divide(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number DivideBy(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Number Negate()
        {
            throw new System.NotImplementedException();
        }

        protected override Boolean LessThan(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Boolean BiggerThan(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Boolean LessThanOrEqual(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Boolean BiggerThanOrEqual(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override Boolean EqualsNumber(Number num)
        {
            throw new System.NotImplementedException();
        }

        protected override int Compare(Number num)
        {
            throw new System.NotImplementedException();
        }
    }
}
