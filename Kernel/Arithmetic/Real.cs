using Kernel.BaseTypes;
using System;
using System.Collections.Generic;
namespace Kernel.Arithmetic
{
    public sealed class Real : Number
    {
        public static bool HasPrimaryValue(Real real)
        => real.Exact || (!double.IsNaN((real.Data as InexactReal).PrimaryValue));

        public override NumberHierarchy Priority => NumberHierarchy.Real;

        Number Data { get; }

        static readonly Dictionary<Number, Real> cache = new Dictionary<Number, Real>();

        public static readonly Real PositiveInfinity = Get(double.PositiveInfinity);

        public static readonly Real NegativeInfinity = Get(double.NegativeInfinity);

        Real(double value)
        {
            Data = InexactReal.Get(value);
        }

        Real(decimal value)
        {
            Data = ExactReal.Get(value);
        }

        Real(Number number)
        {
            Data = number;
        }

        public static Real Get(double value)
        {
            InexactReal container = InexactReal.Get(value);
            if (cache.ContainsKey(container)) return cache[container];
            cache.Add(container, new Real(value));
            return cache[container];
        }

        public static Real Get(string value, int @base = 10)
        {
            if (value.Contains("#"))
            {
                double lowerBound = double.Parse(value.Replace('#', '0'));

                char replace = @base > 16 ? (char)('a' + (@base - 11)) : (char)('0' + @base - 1);
                double upperBound = double.Parse(value.Replace('#', replace));

                double primaryValue = (lowerBound + upperBound) / 2;

                InexactReal key = InexactReal.Get(lowerBound, upperBound, primaryValue, false);
                if (cache.ContainsKey(key))
                    return cache[key];
                Real result = new Real(key);
                cache.Add(key, result);
                return result;
            }
            else
            {
                ExactReal key = ExactReal.Get(decimal.Parse(Integer.Get(value, @base).ToString()));
                if (cache.ContainsKey(key))
                    return cache[key];
                Real result = new Real(key);
                cache.Add(key, result);
                return result;
            }
        }

        public static Real Convert(Number number)
        {
            switch (number)
            {
                case Integer integer:
                    return Get((decimal)integer);
                case Rational rational:
                    return Get(((decimal)rational.Numerator) / ((decimal)rational.Denominator));
                case Real real:
                    return real;
                case InexactReal inexact:
                    if (cache.ContainsKey(inexact))
                        return cache[inexact];
                    Real resultInexact = new Real(inexact);
                    cache.Add(inexact, resultInexact);
                    return resultInexact;
                case ExactReal exact:
                    if (cache.ContainsKey(exact))
                        return cache[exact];
                    Real resultExact = new Real(exact);
                    cache.Add(exact, resultExact);
                    return resultExact;
            }
            throw new InvalidOperationException("Is this a complex??!");
        }

        public static Real Get(decimal value)
        {
            Number cachedKey = ExactReal.Get(value);
            if (cache.ContainsKey(cachedKey))
                return cache[cachedKey];
            Real result = new Real(cachedKey);
            cache.Add(cachedKey, result);
            return result;
        }

        public static Real Get(Number number)
        {
            switch (number)
            {
                case InexactReal inexact:
                    if (cache.ContainsKey(inexact))
                        return cache[inexact];
                    Real resultInexact = new Real(inexact);
                    cache.Add(inexact, resultInexact);
                    return resultInexact;
                case ExactReal exact:
                    if (cache.ContainsKey(exact))
                        return cache[exact];
                    Real resultExact = new Real(exact);
                    cache.Add(exact, resultExact);
                    return resultExact;
            }
            throw new InvalidOperationException("Is this a complex??!");
        }


        public override string ToString() => Data.ToString();

        protected override Number Add(Number num) => Get(Data + Convert(num).Data);

        protected override Number Subtract(Number num) => Get(Data - Convert(num).Data);

        protected override Number SubtractFrom(Number num) => Get(Convert(num).Data - Data);

        protected override Number Multiply(Number num) => Get(Convert(num).Data * Data);

        protected override Number Divide(Number num) => Get(Convert(num).Data / Data);

        protected override Number DivideBy(Number num) => Get(Data / Convert(num).Data);

        protected override Number Negate() => Get(-Data);

        protected override Boolean LessThan(Number num) => Data < Convert(num).Data;

        protected override Boolean BiggerThan(Number num) => Data > Convert(num).Data;

        protected override Boolean LessThanOrEqual(Number num) => Data <= Convert(num).Data;

        protected override Boolean BiggerThanOrEqual(Number num) => Data >= Convert(num).Data;

        public static Integer Ceiling(Real x)
        {
            if (!x.Exact)
                throw new ArgumentException("Input is not an exact real", nameof(x));
            return Integer.Get(Math.Ceiling((x.Data as ExactReal).Data).ToString());
        }

        public static Integer Floor(Real x)
        {
            if (!x.Exact)
                throw new ArgumentException("Input is not an exact real", nameof(x));
            return Integer.Get(Math.Floor((x.Data as ExactReal).Data).ToString());
        }

        public static implicit operator Real(Rational rational) => Get((double)rational.Numerator / (double)rational.Denominator);
        public static implicit operator Real(Integer integer) => Get((double)integer.Data);
        public static implicit operator Real(double dub) => Get(dub);
        public static implicit operator Real(decimal dec) => Get(dec);

        protected override int Compare(Number num)
        {
            Real r = Convert(num);

            return ReferenceEquals(this, r) ? 0 : BiggerThan(r) ? 1 : -1;
        }

        public static explicit operator decimal(Real real)
        {
            if (real.Data is ExactReal exact)
                return exact.Data;
            throw new ArgumentException("WTF?!");
        }

        class ExactReal : Number
        {
#pragma warning disable RECS0146 // Member hides static member from outer class
            public static readonly IDictionary<decimal, ExactReal> cache = new Dictionary<decimal, ExactReal>();
#pragma warning restore RECS0146 // Member hides static member from outer class
            public decimal Data { get; }
            public override NumberHierarchy Priority => NumberHierarchy.Integer;
            ExactReal(decimal value)
            {
                Data = value;
            }

#pragma warning disable RECS0146 // Member hides static member from outer class
            public static ExactReal Get(decimal value)
#pragma warning restore RECS0146 // Member hides static member from outer class
            {
                if (cache.ContainsKey(value)) return cache[value];
                ExactReal real = new ExactReal(value);
                cache.Add(value, real);
                return real;
            }

            protected override Number Add(Number num)
            => Get(Data + (num as ExactReal).Data);

            protected override Number Subtract(Number num)
            => Get(Data - (num as ExactReal).Data);

            protected override Number SubtractFrom(Number num)
            => Get((num as ExactReal).Data - Data);

            protected override Number Multiply(Number num)
            => Get(Data * (num as ExactReal).Data);

            protected override Number Divide(Number num)
            => Get(Data / (num as ExactReal).Data);

            protected override Number DivideBy(Number num)
            => Get((num as ExactReal).Data / Data);

            protected override Number Negate()
            => Get(-Data);

            protected override Boolean LessThan(Number num)
            => Data < (num as ExactReal).Data;

            protected override Boolean BiggerThan(Number num)
            => Data > (num as ExactReal).Data;

            protected override Boolean LessThanOrEqual(Number num)
            => Data <= (num as ExactReal).Data;

            protected override Boolean BiggerThanOrEqual(Number num)
            => Data >= (num as ExactReal).Data;

            protected override int Compare(Number num)
            => Data.CompareTo((num as ExactReal).Data);

            public override string ToString() => Data.ToString();
        }

        class InexactReal : Number
        {
            public static readonly InexactReal Undefined = new InexactReal(double.NegativeInfinity, double.PositiveInfinity);
#pragma warning disable RECS0146 // Member hides static member from outer class
            static readonly IDictionary<(double, double, double, bool), InexactReal> cache
#pragma warning restore RECS0146 // Member hides static member from outer class
            = new Dictionary<(double, double, double, bool), InexactReal>();
            public override NumberHierarchy Priority => NumberHierarchy.Real;

            double UpperBound { get; }
            double LowerBound { get; }
            public double PrimaryValue { get; }
            bool Robust { get; }

            InexactReal(double value)
                : this(value, value, value, true)
            {
            }

            InexactReal(double lowerBound, double upperBound, double primaryValue = double.NaN, bool robust = false)
            {
                LowerBound = lowerBound;
                UpperBound = upperBound;
                PrimaryValue = primaryValue;
                Robust = robust;
            }

#pragma warning disable RECS0146 // Member hides static member from outer class
            public static InexactReal Get(double lowerBound, double upperBound, double primaryValue, bool robust)
#pragma warning restore RECS0146 // Member hides static member from outer class
            {
                if (upperBound < lowerBound)
                    return Undefined;
                var key = (lowerBound, upperBound, primaryValue, robust);
                if (cache.ContainsKey(key))
                    return cache[key];
                InexactReal result = new InexactReal(lowerBound, upperBound, primaryValue, robust);
                cache.Add(key, result);
                return result;
            }
#pragma warning disable RECS0146 // Member hides static member from outer class
            public static InexactReal Get(double value)
            => Get(value, value, value, true);

            public static InexactReal Get(Number number)
#pragma warning restore RECS0146 // Member hides static member from outer class
            {
                switch (number)
                {
                    case Real real:
                        return Get(real.Data);
                    case ExactReal exact:
                        return Get((double)exact.Data);
                    case InexactReal inexact:
                        return inexact;
                    case Integer integer:
                        return Get((double)integer);
                    case Rational rational:
                        return Get(((double)rational.Numerator) / ((double)rational.Denominator));
                }
                throw new Exception("WTF?!");
            }

            protected override Number Add(Number num)
            {
                InexactReal other = Get(num);
                return Get(LowerBound + other.LowerBound, UpperBound + other.UpperBound, PrimaryValue + other.PrimaryValue, Robust && other.Robust);
            }

            protected override Number Subtract(Number num)
            {
                InexactReal other = Get(num);
                return Get(LowerBound - other.LowerBound, UpperBound - other.UpperBound, PrimaryValue - other.PrimaryValue, Robust && other.Robust);
            }

            protected override Number SubtractFrom(Number num)
            {
                InexactReal other = Get(num);
                return Get(other.LowerBound - LowerBound, other.UpperBound - UpperBound, other.PrimaryValue - PrimaryValue, Robust && other.Robust);
            }

            protected override Number Multiply(Number num)
            {
                InexactReal other = Get(num);
                return Get(other.LowerBound * LowerBound, other.UpperBound * UpperBound, other.PrimaryValue * PrimaryValue, Robust && other.Robust);
            }

            protected override Number Divide(Number num)
            {
                InexactReal other = Get(num);
                if (0 >= LowerBound && 0 <= UpperBound)
                {
                    throw new ArithmeticException("Cannot divide by an inexact number which contains 0");
                }
                return Get(other.LowerBound / LowerBound, other.UpperBound / UpperBound, other.PrimaryValue / PrimaryValue, Robust && other.Robust);

            }

            protected override Number DivideBy(Number num)
            {
                InexactReal other = Get(num);

                if (0 >= other.LowerBound && 0 <= other.UpperBound)
                {
                    throw new ArithmeticException("Cannot divide by an inexact number which contains 0");
                }
                return Get(LowerBound / other.LowerBound, UpperBound / other.UpperBound, PrimaryValue / other.PrimaryValue, Robust && other.Robust);
            }

            protected override Number Negate()
            => Get(-LowerBound, -UpperBound, -PrimaryValue, Robust);

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

            protected override int Compare(Number num)
            {
                throw new System.NotImplementedException();
            }
            public override string ToString() => $"{PrimaryValue}";
        }

        internal static bool IsUndefined(Real r)
        => (!r.Exact) && (r.Data as InexactReal) == InexactReal.Undefined;
    }

}