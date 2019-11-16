using System.Numerics;
using System.Collections.Generic;
using Kernel.BaseTypes;

namespace Kernel.Arithmetic
{
    /// <summary>
    /// Rational number, consisting of a numerator and denominator.
    /// </summary>
    public sealed class Rational : Number
    {
        public override NumberHierarchy Priority => NumberHierarchy.Rational;

        static readonly IDictionary<(Integer numerator, Integer denominator), Rational> cache
        = new Dictionary<(Integer, Integer), Rational>();

        /// <summary>
        /// The numerator.
        /// </summary>
        public Integer Numerator { get; }

        /// <summary>
        /// The denominator.
        /// </summary>
        public Integer Denominator { get; }

        Rational(Integer numerator, Integer denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public static Rational Get(Integer numerator) => Get(numerator, BigInteger.One);
        public static Rational Get(Integer numerator, Integer denominator)
        {
            if (denominator == 0)
                throw new System.ArgumentOutOfRangeException(nameof(denominator), "Denominator cannot be zero!");
            Integer divisor = NumericOperations.GCD(numerator, denominator);
            Integer numeratorLowered = numerator.Div(divisor);
            Integer denominatorLowered = denominator.Div(divisor);
            var key = (numeratorLowered, denominatorLowered);
            if (cache.ContainsKey(key))
                return cache[key];
            Rational number = new Rational(numeratorLowered, denominatorLowered);
            cache.Add(key, number);
            return number;
        }

        public static Number Get(string numerator, string denominator, int numeratorBase = 10, int denominatorBase = 10)
        => Get(Integer.Get(numerator, numeratorBase),
               Integer.Get(denominator, denominatorBase));


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Rational"/>.</returns>
        public override string ToString() => $"{Numerator}{ (Denominator == 1 ? "" : "/" + Denominator) }";

        protected override Number Add(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(Numerator * 2, Denominator);
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            Integer sum = (Numerator * lcm).Div(Denominator) + (other.Numerator * lcm).Div(other.Denominator);
            return Get(sum, lcm);
        }

        protected override Number Subtract(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(0);
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            Integer diff = (Numerator * lcm).Div(Denominator) - (other.Numerator * lcm).Div(other.Denominator);
            return Get(diff, lcm);
        }

        protected override Number SubtractFrom(Number num)
        {
            if (ReferenceEquals(this, num)) return Get(0);
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            Integer diff = (other.Numerator * lcm).Div(other.Denominator) - (Numerator * lcm).Div(Denominator);
            return Get(diff, lcm);
        }

        protected override Number Multiply(Number num)
        {
            Rational other = Convert(num);
            return Get(Numerator * other.Numerator, Denominator * other.Denominator);
        }

        protected override Number DivideBy(Number num)
        {
            Rational other = Convert(num);
            return Get(Numerator * other.Denominator, Denominator * other.Numerator);
        }

        protected override Number Divide(Number num)
        {
            Rational other = Convert(num);
            return Get(other.Numerator * Denominator, other.Denominator * Numerator);
        }

        protected override Number Negate() => Get(-Numerator as Integer, Denominator);

        protected override Boolean LessThan(Number num)
        {
            if (ReferenceEquals(this, num)) return false;
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator < other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean BiggerThan(Number num)
        {
            if (ReferenceEquals(this, num)) return false;
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator > other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean LessThanOrEqual(Number num)
        {
            if (ReferenceEquals(this, num)) return true;
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator <= other.Numerator * lcm / other.Denominator;
        }

        protected override Boolean BiggerThanOrEqual(Number num)
        {
            if (ReferenceEquals(this, num)) return true;
            Rational other = Convert(num);
            Integer lcm = NumericOperations.LCM(Denominator, other.Denominator);
            return Numerator * lcm / Denominator >= other.Numerator * lcm / other.Denominator;
        }

        protected override int Compare(Number num) => ReferenceEquals(this, num) ? 0 : BiggerThan(num) ? 1 : -1;

        static Rational Convert(Number num)
        {
            if (num is Rational rat) return rat;
            if (num is Integer integer) return Get(integer);
            throw new System.ArgumentException("WTF");
        }

        public static implicit operator Rational(Integer integer) => Get(integer);

        static class NumericOperations
        {
            public static BigInteger GCD(BigInteger l, BigInteger r) => BigInteger.GreatestCommonDivisor(l, r);
            public static BigInteger LCM(BigInteger l, BigInteger r) => l * r / GCD(l, r);
        }
    }
}
