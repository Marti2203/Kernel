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

        public BigInteger Data => data;
        readonly BigInteger data;

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
            if (!((result = cache.Values.FirstOrDefault(v => v.Data == input)) is null))
                return result;
            result = new Integer(input);
            cache.Add(input.ToString(), result);
            return result;
        }

        public override string ToString() => Data.ToString();

        public static bool operator >(Integer l, Integer r) => !(r is null) && l.Data > r.Data;

        public static bool operator <(Integer l, Integer r) => !(r is null) && l.Data < r.Data;

        public static bool operator >=(Integer l, Integer r) => !(r is null) && l.Data >= r.Data;

        public static bool operator <=(Integer l, Integer r) => !(r is null) && l.Data <= r.Data;

        public static bool operator ==(Integer l, Integer r) => !(r is null) && l.Data == r.Data;

        public static bool operator !=(Integer l, Integer r) => !(r is null) && l.Data != r.Data;

        public static Integer operator %(Integer l, Integer r) => l.Data % r.Data;

        public override int GetHashCode() => Data.GetHashCode();

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            if (n.Priority > Priority) return n.Equals(this);
            return (n as Integer).Data == Data;
        }

        protected override Number Add(Number num) => Get(Data + (num as Integer).Data);

        protected override Number Subtract(Number num) => Get(Data - (num as Integer).Data);

        protected override Number SubtractFrom(Number num) => Get((num as Integer).Data - Data);

        protected override Number Multiply(Number num) => Get((num as Integer).Data * Data);

        protected override Number Divide(Number num)
        => Data == 0 ? throw new System.ArgumentException("Cannot divide an integer by zero!")
                                           : Get((num as Integer).Data / Data);

        protected override Number DivideBy(Number num)
        => (num as Integer).Data == 0 ? throw new System.ArgumentException("Cannot divide an integer by zero!")
                                           : Get(Data / (num as Integer).Data);

        protected override Number Negate() => Get(-Data);

        protected override Boolean LessThan(Number num) => Data < (num as Integer).Data;

        protected override Boolean BiggerThan(Number num) => Data > (num as Integer).Data;

        protected override Boolean LessThanOrEqual(Number num) => Data <= (num as Integer).Data;

        protected override Boolean BiggerThanOrEqual(Number num) => Data >= (num as Integer).Data;

        protected override int Compare(Number num) => (Data - (num as Integer).Data).Sign;

        public static implicit operator BigInteger(Integer @int) => @int.Data;
        public static implicit operator Integer(BigInteger @int) => Get(@int);
        public static implicit operator Integer(long number) => Get(number);
        public static explicit operator int(Integer @int)
        => @int.Data > int.MaxValue ?
                throw new System.InvalidCastException("Value is bigger than max integer size")
                   : (int)@int.Data;
        public static explicit operator long(Integer @int)
        => @int.Data > long.MaxValue ?
        throw new System.InvalidCastException("Value is bigger than max long size")
                   : (long)@int.Data;
    }
}
