using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace Kernel.Arithmetic
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    /// <summary>
    /// Integer class
    /// </summary>
    public sealed class Integer : Number
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        static readonly string digits = "0123456789abcdef";
        static readonly Dictionary<char, int> values = digits.ToDictionary(c => c, digits.IndexOf);
        static BigInteger ParseBigInteger(string value, int baseOfValue)
        => value.Aggregate(new BigInteger(), (current, digit) => current * baseOfValue + values[digit]);

        static readonly Dictionary<string, Integer> cache = new Dictionary<string, Integer>{
            {"0",Zero},
            {"1",One},
        };
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

            if (cache.TryGetValue(input, out Integer v))
                return v;

            var integer = new Integer(result);
            cache.Add(integer.ToString(), integer);
            return integer;
        }

        public static Integer Get(long input) => Get(new BigInteger(input));

        public static Integer Get(BigInteger input)
        {
            Integer result;
            if (!((result = cache.Values.FirstOrDefault(v => v.data == input)) is null))
                return result;
            result = new Integer(input);
            cache.Add(input.ToString(), result);
            return result;
        }

        public override string ToString() => data.ToString();

        public static bool operator >(Integer l, Integer r) => !(r is null) && l.data > r.data;

        public static bool operator <(Integer l, Integer r) => !(r is null) && l.data < r.data;

        public static bool operator >=(Integer l, Integer r) => !(r is null) && l.data >= r.data;

        public static bool operator <=(Integer l, Integer r) => !(r is null) && l.data <= r.data;

        public static bool operator ==(Integer l, Integer r) => !(r is null) && l.data == r.data;

        public static bool operator !=(Integer l, Integer r) => !(r is null) && l.data != r.data;

        public static Integer operator %(Integer l, Integer r) => l.data % r.data;

        public override int GetHashCode() => data.GetHashCode();

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            return (n as Integer).data == data;
        }

        protected override Number Add(Number num) => Get(data + (num as Integer).data);

        protected override Number Subtract(Number num) => Get(data - (num as Integer).data);

        protected override Number SubtractFrom(Number num) => Get((num as Integer).data - data);

        protected override Number Multiply(Number num) => Get((num as Integer).data * data);

        protected override Number Divide(Number num)
        => data == 0 ? throw new System.ArgumentException("Cannot divide an integer by zero!")
                                           : Get((num as Integer).data / data);

        protected override Number DivideBy(Number num)
        => (num as Integer).data == 0 ? throw new System.ArgumentException("Cannot divide an integer by zero!")
                                           : Get(data / (num as Integer).data);

        protected override Number Negate() => Get(-data);

        protected override Boolean LessThan(Number num) => data < (num as Integer).data;

        protected override Boolean BiggerThan(Number num) => data > (num as Integer).data;

        protected override Boolean LessThanOrEqual(Number num) => data <= (num as Integer).data;

        protected override Boolean BiggerThanOrEqual(Number num) => data >= (num as Integer).data;

        protected override Boolean EqualsNumber(Number num) => data == (num as Integer).data;

        protected override int Compare(Number num) => (data - (num as Integer).data).Sign;

        public static implicit operator Rational(Integer @int) => new Rational(@int.data);
        public static implicit operator BigInteger(Integer @int) => @int.data;
        public static implicit operator Integer(BigInteger @int) => Get(@int);
        public static implicit operator Integer(long number) => Get(number);
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
