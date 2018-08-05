using System;
using System.Collections.Generic;
namespace Kernel.Arithmetic
{
	/// <summary>
	/// Complex number, consisting of a real and imaginary part.
	/// Highest number in hierarchy
	/// </summary>
	public sealed class Complex : Number
	{
		static readonly Dictionary<Tuple<decimal, decimal>, Complex> cache = new Dictionary<Tuple<decimal, decimal>, Complex>();

		public override NumberHierarchy Priority => NumberHierarchy.Complex;

		public readonly decimal real;
		public readonly decimal imaginary;

		Complex(decimal real, decimal imaginary)
		{
			this.real = real;
			this.imaginary = imaginary;
		}

		public static Complex Get(decimal real, decimal imaginary)
		{
			var key = Tuple.Create(real, imaginary);
			if (cache.ContainsKey(key)) return cache[key];
			cache.Add(key, new Complex(real, imaginary));
			return cache[key];
		}

		public override string ToString()
		=> imaginary == 0 ? real.ToString() : $"{real}{ (imaginary > 0 ? "+" : "-") }{imaginary}i";

		public override bool Equals(Object other)
		{
			if (!(other is Number n)) return false;
			if (n.Exact != Exact) return false;
			Complex complex = (Complex)other;
			return real == complex.real && imaginary == complex.imaginary;
		}

		protected override Number Add(Number num)
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

		protected override Number Multiply(Number num)
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

		protected override Number Negate()
		{
			throw new NotImplementedException();
		}
	}
}