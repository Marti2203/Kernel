using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Number.
    /// </summary>
    public abstract class Number : Object
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

        public static Number operator -(Number l) => l.Negate();

        public static Number operator +(Number l, Number r)
        {
            if (l.Priority > r.Priority) return l.Add(r);
            return r.Add(l);
        }

        public static Number operator -(Number l, Number r)
        {
            if (l.Priority > r.Priority) return l.Subtract(r);
            return r.SubtractFrom(l);
        }

        public static Number operator *(Number l, Number r)
        {
            if (l.Priority > r.Priority) return l.Multiply(r);
            return r.Multiply(l);
        }

        public static Number operator /(Number l, Number r)
        {
            if (l.Priority > r.Priority) return l.DivideBy(r);
            return r.Divide(l);
        }

    }
    public enum NumberHierarchy : byte
    {
        Integer, Rational, Real, Complex
    }
}
