using System.Collections.Generic;
namespace Kernel.Arithmetic
{

    public sealed class Real : Number
    {
        static readonly Dictionary<double, Real> cache = new Dictionary<double, Real>();

        public override NumberHierarchy Priority => NumberHierarchy.Real;

        public double Data => data;
        readonly double data;

        public static readonly Real PositiveInfinity = Get(double.PositiveInfinity);

        public static readonly Real NegativeInfinity = Get(double.NegativeInfinity);

        Real(double value)
        {
            data = value;
        }

        public static Real Get(double value)
        {
            if (cache.ContainsKey(value)) return cache[value];
            cache.Add(value, new Real(value));
            return cache[value];
        }

        public override string ToString() => Data.ToString();

        protected override Number Add(Number num) => Get(Data + (num as Real).Data);

        protected override Number Subtract(Number num) => Get(Data - (num as Real).Data);

        protected override Number SubtractFrom(Number num) => Get((num as Real).Data - Data);

        protected override Number Multiply(Number num) => Get((num as Real).Data * Data);

        protected override Number Divide(Number num) => Get((num as Real).Data / Data);

        protected override Number DivideBy(Number num) => Get(Data / (num as Real).Data);

        protected override Number Negate() => Get(-Data);

        protected override Boolean LessThan(Number num) => Data < (num as Real).Data;

        protected override Boolean BiggerThan(Number num) => Data > (num as Real).Data;

        protected override Boolean LessThanOrEqual(Number num) => Data <= (num as Real).Data;

        protected override Boolean BiggerThanOrEqual(Number num) => Data >= (num as Real).Data;

        public static implicit operator Real(Rational rational) => Get((double)rational.Numerator / (double)rational.Denominator);
        public static implicit operator Real(Integer integer) => Get((double)integer.Data);

        protected override int Compare(Number num)
        => ReferenceEquals(this, num) ? 0 : BiggerThan(num) ? 1 : -1;

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            return System.Math.Abs((n as Real).Data - Data) < double.Epsilon;
        }
    }
}
