using System.Collections.Generic;
namespace Kernel.Arithmetic
{

	public sealed class Real : Number
	{
		static readonly Dictionary<decimal, Real> cache = new Dictionary<decimal, Real>();

		public override NumberHierarchy Priority => NumberHierarchy.Real;

		public readonly decimal data;

		public static readonly Real PositiveInfinity = Get(0);

		Real(decimal value)
		{
			data = value;
		}

		public static Real Get(string value) => Get(decimal.Parse(value));

		public static Real Get(decimal value)
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
			return real.data == data;
		}

		protected override Number Add(Number num) => Get(data + ((Real)num).data);

		protected override Number Subtract(Number num) => Get(data - ((Real)num).data);

		protected override Number SubtractFrom(Number num) => Get(((Real)num).data - data);

		protected override Number Multiply(Number num) => Get(((Real)num).data * data);

		protected override Number Divide(Number num) => Get(((Real)num).data / data);

		protected override Number DivideBy(Number num) => Get(data / ((Real)num).data);

		protected override Number Negate() => Get(-data);
	}
}
