using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Number.
    /// </summary>
    public abstract class Number : Object, IEquatable<Number>, IComparable<Number>
    {
        public abstract NumberHierarchy Priority { get; }

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
        protected abstract int Compare(Number num);

        public bool Exact => true;

        public bool Equals(Number other) => ReferenceEquals(this, other);
        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public int CompareTo(Number other)
        => Priority >= other.Priority ? Compare(other) : other.Compare(this);

        public static Number operator -(Number l) => l is null ? throw new NullReferenceException() : l.Negate();

        public static Number operator +(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.Add(r) : r.Add(l);
        }

        public static Number operator -(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.Subtract(r) : r.SubtractFrom(l);
        }

        public static Number operator *(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (l == Integer.Zero)
                return Integer.Zero;
            if (r == Integer.Zero)
                return Integer.Zero;
            if (l == Integer.One)
                return r;
            if (r == Integer.One)
                return l;
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
            return l.Priority >= r.Priority ? l.Multiply(r) : r.Multiply(l);
        }

        public static Number operator /(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.DivideBy(r) : r.Divide(l);
        }

        public static Boolean operator <(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.LessThan(r) : r.BiggerThan(l);
        }
        public static Boolean operator >(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.BiggerThan(r) : r.LessThan(l);
        }
        public static Boolean operator >=(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.BiggerThanOrEqual(r) : r.LessThanOrEqual(l);
        }
        public static Boolean operator <=(Number l, Number r)
        {
            if (l is null || r is null)
                throw new NullReferenceException();
            return l.Priority >= r.Priority ? l.LessThanOrEqual(r) : r.BiggerThanOrEqual(l);
        }

        public static implicit operator Number(long value) => Integer.Get(value);
    }
    public enum NumberHierarchy : byte
    {
        Integer, Rational, Real, Complex
    }
}
