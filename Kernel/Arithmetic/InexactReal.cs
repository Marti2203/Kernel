using System;
using System.Collections.Generic;

namespace Kernel.Arithmetic
{
    public class InexactReal : Number
    {
        static readonly IDictionary<(double lowerBound, double upperBound), InexactReal> cache = new Dictionary<(double, double), InexactReal>();
        public static readonly InexactReal Undefined = new InexactReal(double.PositiveInfinity, double.NegativeInfinity);
        readonly double upperBound;
        readonly double primaryValue;
        readonly bool robustTag;// WTF?
        readonly double lowerBound;

        InexactReal(double lowerBound, double upperBound)
        {
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
            if (Math.Abs(lowerBound - upperBound) < double.Epsilon)
            {
                primaryValue = lowerBound;
                robustTag = true;
            }
            else
            {
                primaryValue = (lowerBound + upperBound) / 2;
            }
        }

        public static InexactReal Get(double lowerBound, double upperBound)
        {
            var range = (lowerBound, upperBound);
            if (lowerBound > upperBound) return Undefined;
            if (cache.ContainsKey(range))
                return cache[range];
            InexactReal result = new InexactReal(lowerBound, upperBound);
            cache.Add(range, result);
            return result;
        }

        public override NumberHierarchy Priority => throw new NotImplementedException();

        public override bool Equals(Object other)
        {
            throw new NotImplementedException();
        }

        protected override Number Add(Number num)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override Number DivideBy(Number num)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override Number Negate()
        {
            throw new NotImplementedException();
        }

        protected override Number Subtract(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Number SubtractFrom(Number num)
        {
            throw new NotImplementedException();
        }
    }
}
