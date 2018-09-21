using System.Numerics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Kernel.Arithmetic
{
	/// <summary>
	/// Integer class
	/// </summary>
	public sealed class Integer : Number
	{
		static readonly string digits = "0123456789abcdef";
		static readonly Dictionary<char, int> values = digits.ToDictionary(c => c, digits.IndexOf);
		static BigInteger ParseBigInteger(string value, int baseOfValue)
		=> value.Aggregate(new BigInteger(), (current, digit) => current * baseOfValue + values[digit]);

		static readonly Dictionary<int, Integer> smallStringCache = new Dictionary<int, Integer>();
		public override NumberHierarchy Priority => NumberHierarchy.Integer;

		public readonly BigInteger data;

		public static Integer Zero => new Integer(BigInteger.Zero);
		public static Integer One => new Integer(BigInteger.One);

		Integer(BigInteger value)
		{
			data = value;
		}

		public static Integer Get(string input, int @base = 10)
		{
			BigInteger result;
			if (input[0] == '-' || input[0] == '+')
				result = ParseBigInteger(input.Substring(1), @base) * (input[0] == '-' ? -1 : 1);
			else
				result = ParseBigInteger(input, @base);
			return new Integer(result);
		}

		public static Integer Get(long input)
		{
			return new Integer(input);
		}

		public override string ToString() => data.ToString();

		public static bool operator >(Integer l, Integer r) => l.data > r.data;

		public static bool operator <(Integer l, Integer r) => l.data < r.data;

		public static bool operator >=(Integer l, Integer r) => l.data >= r.data;

		public static bool operator <=(Integer l, Integer r) => l.data <= r.data;

		public static bool operator ==(Integer l, Integer r) => l.data == r.data;

		public static bool operator !=(Integer l, Integer r) => l.data != r.data;

		public override int GetHashCode() => data.GetHashCode();

		public override bool Equals(Object other)
		{
			if (!(other is Number n)) return false;
			if (n.Exact != Exact) return false;
			if (n.Priority > Priority) return n.Equals(this);
			return (n as Integer).data == data;
		}

		protected override Number Add(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number Subtract(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number SubtractFrom(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number Multiply(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number Divide(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number DivideBy(Number num)
		{
			throw new System.NotImplementedException();
		}

		protected override Number Negate()
		{
			throw new System.NotImplementedException();
		}

		public static implicit operator Rational(Integer @int) => new Rational(@int.data);
		public static implicit operator BigInteger(Integer @int) => @int.data;
		public static implicit operator Integer(int number) => Get(number);
		public static explicit operator int(Integer @int)
		=> @int.data > int.MaxValue ?
				throw new System.InvalidCastException("Value is bigger than max integer size")
				   : (int)@int.data;
		public static explicit operator long(Integer @int)
		=> @int.data > long.MaxValue ?
		throw new System.InvalidCastException("Value is bigger than max long size")
				   : (long)@int.data;
	}
}
