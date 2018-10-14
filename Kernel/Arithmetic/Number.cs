using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Number.
    /// </summary>
    public abstract class Number : Object, IEquatable<Number>, IComparable<Number>
    {
        public abstract NumberHierarchy Priority { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Kernel.Arithmetic.Number"/> is exact.
        /// </summary>
        /// <value><c>true</c> if exact; otherwise, <c>false</c>.</value>
        public bool Exact { get; set; }

        protected abstract Number Add(Number num);
        protected abstract Number Subtract(Number num);
        protected abstract Number SubtractFrom(Number num);
        protected abstract Number Multiply(Number num);
        protected abstract Number Divide(Number num);
        protected abstract Number DivideBy(Number num);
        protected abstract Number Negate();
        protected abstract Boolean LessThan(Number num);
        protected abstract Boolean BiggerThan(Number num);
        protected abstract Boolean LessThanOrEqual(Number num);
        protected abstract Boolean BiggerThanOrEqual(Number num);
        protected abstract Boolean EqualsNumber(Number num);
        protected abstract int Compare(Number num);

        public bool Equals(Number other)
        => Priority < other.Priority ? other.EqualsNumber(this) : EqualsNumber(other);

        public int CompareTo(Number other)
        {
            if (Priority >= other.Priority) return Compare(other);
            return other.Compare(this);
        }

        public static Number operator -(Number l) => l.Negate();

        public static Number operator +(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.Add(r);
            return r.Add(l);
        }

        public static Number operator -(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.Subtract(r);
            return r.SubtractFrom(l);
        }

        public static Number operator *(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.Multiply(r);
            return r.Multiply(l);
        }

        public static Number operator /(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.DivideBy(r);
            return r.Divide(l);
        }

        public static Boolean operator <(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.LessThan(r);
            return r.BiggerThan(l);
        }
        public static Boolean operator >(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.BiggerThan(r);
            return r.LessThan(l);
        }
        public static Boolean operator >=(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.BiggerThanOrEqual(r);
            return r.LessThanOrEqual(l);
        }
        public static Boolean operator <=(Number l, Number r)
        {
            if (l.Priority >= r.Priority) return l.LessThanOrEqual(r);
            return r.BiggerThanOrEqual(l);
        }
    }
    public enum NumberHierarchy : byte
    {
        Integer, Rational, Real, Complex
    }
}
