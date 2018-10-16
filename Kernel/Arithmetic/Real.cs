﻿using System.Collections.Generic;
namespace Kernel.Arithmetic
{

    public sealed class Real : Number
    {
        static readonly Dictionary<double, Real> cache = new Dictionary<double, Real>();

        public override NumberHierarchy Priority => NumberHierarchy.Real;

        public readonly double data;

        public static readonly Real PositiveInfinity = Get(double.PositiveInfinity);

        public static readonly Real NegativeInfinity = Get(double.NegativeInfinity);

        Real(double value)
        {
            data = value;
        }

        public static Real Get(string value) => Get(double.Parse(value));

        public static Real Get(double value)
        {
            if (cache.ContainsKey(value)) return cache[value];
            cache.Add(value, new Real(value));
            return cache[value];
        }

        public override string ToString() => data.ToString();

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            Real real = (n as Real);
            return System.Math.Abs(real.data - data) < double.Epsilon;
        }

        protected override Number Add(Number num) => Get(data + (num as Real).data);

        protected override Number Subtract(Number num) => Get(data - (num as Real).data);

        protected override Number SubtractFrom(Number num) => Get((num as Real).data - data);

        protected override Number Multiply(Number num) => Get((num as Real).data * data);

        protected override Number Divide(Number num) => Get((num as Real).data / data);

        protected override Number DivideBy(Number num) => Get(data / (num as Real).data);

        protected override Number Negate() => Get(-data);

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
    }
}
