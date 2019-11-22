using Kernel.BaseTypes;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Number.
    /// </summary>
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public abstract class Number : Object, System.IEquatable<Number>, System.IComparable<Number>
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
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

        public static bool operator ==(Number l, Number r) => ReferenceEquals(l, r) || l.CompareTo(r) == 0;
        public static bool operator !=(Number l, Number r) => !ReferenceEquals(l, r) && l.CompareTo(r) != 0;

        public bool Equals(Number other) => ReferenceEquals(this, other);
        public override bool Equals(Object other) => ReferenceEquals(this, other) || (GetType() == other.GetType() && InternalEquals(other) ) 
                                                    || (other is Number n && CompareTo(n) == 0) ;

        protected abstract bool InternalEquals(Object other);

        public int CompareTo(Number other)
        => Priority >= other.Priority ? Compare(other) : other.Compare(this);

        public static Number operator -(Number l) => l is null ? throw new System.NullReferenceException() : l.Negate();

        public static Number operator +(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.Add(r) : r.Add(l);
        }

        public static Number operator -(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.Subtract(r) : r.SubtractFrom(l);
        }

        public static Number operator *(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (ReferenceEquals(l, Integer.Zero))
                return Integer.Zero;
            if (ReferenceEquals(r, Integer.Zero))
                return Integer.Zero;
            if (ReferenceEquals(l, Integer.One))
                return r;
            if (ReferenceEquals(r, Integer.One))
                return l;
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
            return l.Priority >= r.Priority ? l.Multiply(r) : r.Multiply(l);
        }

        public static Number operator /(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.DivideBy(r) : r.Divide(l);
        }

        public static Boolean operator <(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.LessThan(r) : r.BiggerThan(l);
        }
        public static Boolean operator >(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.BiggerThan(r) : r.LessThan(l);
        }
        public static Boolean operator >=(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.BiggerThanOrEqual(r) : r.LessThanOrEqual(l);
        }
        public static Boolean operator <=(Number l, Number r)
        {
            if (l is null || r is null)
                throw new System.NullReferenceException();
            return l.Priority >= r.Priority ? l.LessThanOrEqual(r) : r.BiggerThanOrEqual(l);
        }

        public static implicit operator Number(long value) => Integer.Get(value);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            if (obj is Object o)
                return Equals(o);
            return false;
        }
    }
    public enum NumberHierarchy : byte
    {
        Integer, Rational, Real, Complex
    }
}
