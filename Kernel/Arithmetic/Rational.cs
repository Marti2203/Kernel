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

        static IDictionary<(BigInteger numerator, BigInteger denominator), Rational> cache
        = new Dictionary<(BigInteger, BigInteger), Rational>();

        /// <summary>
        /// The numerator.
        /// </summary>
        public BigInteger Numerator => numerator;
        readonly BigInteger numerator;

        /// <summary>
        /// The denominator.
        /// </summary>
        public BigInteger Denominator => denominator;
        readonly BigInteger denominator;

        Rational(BigInteger numerator)
            : this(numerator, BigInteger.One) { }

        Rational(BigInteger numerator, BigInteger denominator)
        {
            this.denominator = denominator / denominator;
            this.numerator = numerator / numerator;
        }

        public static Rational Get(BigInteger numerator) => Get(numerator, BigInteger.One);
        public static Rational Get(BigInteger numerator, BigInteger denominator)
        {
            if (denominator == 0)
                throw new System.ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero!");
            BigInteger divisor = NumericOperations.GCD(numerator, denominator);
            BigInteger numeratorLowered = numerator / divisor;
            BigInteger denominatorLowered = denominator / divisor;
            var key = (numeratorLowered, denominatorLowered);
            if (cache.ContainsKey(key))
                return cache[key];
            Rational number = new Rational(numeratorLowered, denominatorLowered);
            cache.Add(key, number);
            return number;
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.</returns>
        public override string ToString() => $"{Numerator}{ (Denominator == 1 ? "" : "/" + Denominator) }";

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            Rational rat = (n as Rational);
            return rat.Numerator == Numerator && rat.Denominator == Denominator;
        }

        protected override Number Add(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(Numerator * 2, Denominator);
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            BigInteger sum = Numerator * lcm / Denominator + other.Numerator * lcm / other.Denominator;
            return Get(sum, lcm);
        }

        protected override Number Subtract(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(0);
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            BigInteger sum = Numerator * lcm / Denominator - other.Numerator * lcm / other.Denominator;
            return Get(sum, lcm);
        }

        protected override Number SubtractFrom(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(0);
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            BigInteger sum = other.Numerator * lcm / other.Denominator - Numerator * lcm / Denominator;
            return Get(sum, lcm);
        }

        protected override Number Multiply(Number num)
        {
            Rational other = num as Rational;
            return Get(Numerator * other.Numerator, Denominator * other.Denominator);
        }

        protected override Number Divide(Number num)
        {
            Rational other = num as Rational;
            return Get(Numerator * other.Denominator, Denominator * other.Numerator);
        }

        protected override Number DivideBy(Number num)
        {
            Rational other = num as Rational;
            return Get(other.Numerator * Denominator, other.Denominator * Numerator);
        }

        protected override Number Negate() => Get(-Numerator, Denominator);

        protected override Boolean LessThan(Number num)
        {
            if (ReferenceEquals(this, num)) return false;
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator < other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean BiggerThan(Number num)
        {
            if (ReferenceEquals(this, num)) return false;
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator > other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean LessThanOrEqual(Number num)
        {
            if (ReferenceEquals(this, num)) return true;
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator <= other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean BiggerThanOrEqual(Number num)
        {
            if (ReferenceEquals(this, num)) return true;
            Rational other = num as Rational;
            BigInteger lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator >= other.Numerator * lcm / other.Denominator;
        }

        protected override int Compare(Number num) => ReferenceEquals(this, num) ? 0 : BiggerThan(num) ? 1 : -1;

        public static implicit operator Rational(Integer integer) => Get(integer.Data);

        static class NumericOperations
        {
            public static BigInteger GCD(BigInteger l, BigInteger r) => BigInteger.GreatestCommonDivisor(l, r);
            public static BigInteger LCM(BigInteger l, BigInteger r) => (l * r) / GCD(l, r);
        }
    }
}
