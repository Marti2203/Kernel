using System;
using System.Collections.Generic;

namespace Kernel.Arithmetic
{
    public class Inexact : Number
    {
        static readonly IDictionary<(double lowerBound, double upperBound), Inexact> cache = new Dictionary<(double, double), Inexact>();
        public static readonly Inexact Undefined = new Inexact(double.PositiveInfinity, double.NegativeInfinity, 0);
        readonly double upperBound;
        readonly double primaryValue;
        readonly double lowerBound;
        readonly bool isRobust;

        public new bool Exact => false;

        Inexact(double lowerBound, double upperBound, double primaryValue, bool isRobust = false)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            this.primaryValue = primaryValue;
            this.isRobust = isRobust;
        }

        public static Inexact Get(string value)
        {
            double parsed = double.Parse(value, System.Globalization.NumberStyles.Any);
            var range = (parsed, parsed);
            if (cache.ContainsKey(range))
                return cache[range];
            Inexact result = new Inexact(parsed, parsed, parsed, parsed.ToString() != value);
            cache.Add(range, result);
            return result;
        }

        public static Inexact Get(double value)
        {
            var range = (value, value);
            if (cache.ContainsKey(range))
                return cache[range];
            Inexact result = new Inexact(value, value, value, true);
            cache.Add(range, result);
            return result;
        }

        public static Inexact Get(double lowerBound, double upperBound)
        {
            var range = (lowerBound, upperBound);
            if (lowerBound > upperBound) return Undefined;
            if (cache.ContainsKey(range))
                return cache[range];
            double primaryValue = double.NaN;
            bool isRobust = false;
            if (Math.Abs(lowerBound - upperBound) < double.Epsilon)
            {
                primaryValue = lowerBound;
                isRobust = true;
            }
            Inexact result = new Inexact(lowerBound, upperBound, primaryValue, isRobust);
            cache.Add(range, result);
            return result;
        }

        static Inexact Get(double lowerBound, double upperBound, double primaryValue, bool isRobust)
        {
            var range = (lowerBound, upperBound);
            if (lowerBound > upperBound) return Undefined;
            if (cache.ContainsKey(range))
            {
                if (Math.Abs(cache[range].primaryValue - primaryValue) < double.Epsilon)
                    return cache[range];
                throw new InvalidOperationException("WHAT");
            }
            Inexact result = new Inexact(lowerBound, upperBound, primaryValue, isRobust);
            cache.Add(range, result);
            return result;
        }

#warning Priority
        public override NumberHierarchy Priority => NumberHierarchy.Real;

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public override string ToString() => ReferenceEquals(this, Undefined) ? "UNDEFINED" : primaryValue.ToString();

        protected override Number Add(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = primaryValue + other.primaryValue;
            return Get(lowerBound + other.lowerBound, upperBound + other.upperBound, newPrimaryValue, isRobust && other.isRobust);
        }

        protected override Boolean BiggerThan(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Boolean BiggerThanOrEqual(Number num)
        {
            throw new NotImplementedException();
        }

        protected override int Compare(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Number Divide(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = primaryValue / other.primaryValue;
            return Get(lowerBound / other.upperBound, upperBound / other.lowerBound, newPrimaryValue, isRobust && other.isRobust);
        }

        protected override Number DivideBy(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = other.primaryValue / primaryValue;
            return Get(other.lowerBound / upperBound, other.upperBound / lowerBound, newPrimaryValue, isRobust && other.isRobust);
        }

        protected override Boolean LessThan(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Boolean LessThanOrEqual(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Number Multiply(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = primaryValue * other.primaryValue;
            return Get(lowerBound * other.lowerBound, upperBound * other.upperBound, newPrimaryValue, isRobust && other.isRobust);
        }

        protected override Number Negate()
        => Get(-upperBound, -lowerBound, -primaryValue, isRobust);

        protected override Number Subtract(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = primaryValue - other.primaryValue;
            return Get(lowerBound - other.upperBound, upperBound - other.lowerBound, newPrimaryValue, isRobust && other.isRobust);
        }

        protected override Number SubtractFrom(Number num)
        {
            if (ReferenceEquals(this, Undefined) || ReferenceEquals(num, Undefined))
                return Undefined;
            Inexact other = num as Inexact;
            double newPrimaryValue = double.NaN;
            if (!(double.IsNaN(primaryValue) || double.IsNaN(other.primaryValue)))
                newPrimaryValue = other.primaryValue - primaryValue;
            return Get(other.lowerBound - upperBound, other.upperBound - lowerBound, newPrimaryValue, isRobust && other.isRobust);
        }

        public static implicit operator Inexact(Integer integer) => Get((double)integer.Data);
        public static implicit operator Inexact(Rational rational) => Get((double)rational.Numerator / (double)rational.Denominator);
    }
}
