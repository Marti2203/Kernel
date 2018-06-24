using System.Numerics;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Integer class
    /// </summary>
    public sealed class Integer : Number
    {
        /// <summary>
        /// The data.
        /// </summary>
        public readonly BigInteger data;

        public static BigInteger Zero => BigInteger.Zero;


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kernel.Arithmetic.Integer"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        public Integer(BigInteger value)
        {
            data = value;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Integer"/>.</returns>
        public override string ToString() => data.ToString();

        /// <summary>
        /// Adds a <see cref="Integer"/> to a <see cref="Kernel.Arithmetic.Integer"/>, yielding a new <see cref="T:Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Integer"/> to add.</param>
        /// <param name="r">The second <see cref="Integer"/> to add.</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Integer"/> that is the sum of the values of <c>l</c> and <c>r</c>.</returns>
        public static Integer operator +(Integer l, Integer r)
        => new Integer(l.data + r.data);

        /// <summary>
        /// Subtracts a <see cref="Integer"/> from a <see cref="Integer"/>, yielding
        /// a new <see cref="T:Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Integer"/> to subtract from (the minuend).</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Integer"/> to subtract (the subtrahend).</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Integer"/> that is the <c>l</c> minus <c>r</c>.</returns>
        public static Integer operator -(Integer l, Integer r)
        => new Integer(l.data - r.data);

        /// <summary>
        /// Computes the product of <c>l</c> and <c>r</c>, yielding a new <see cref="T:Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Integer"/> to multiply.</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Integer"/> to multiply.</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Integer"/> that is the <c>l</c> * <c>r</c>.</returns>
        public static Integer operator *(Integer l, Integer r)
        => new Integer(l.data * r.data);

        /// <summary>
        /// Computes the division of <c>l</c> and <c>r</c>, yielding a new <see cref="T:Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The <see cref="Kernel.Arithmetic.Integer"/> to divide (the divident).</param>
        /// <param name="r">The <see cref="Kernel.Arithmetic.Integer"/> to divide (the divisor).</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Integer"/> that is the <c>l</c> / <c>r</c>.</returns>
        public static Integer operator /(Integer l, Integer r)
        => new Integer(l.data / r.data);

        /// <summary>
        /// Computes the modulo of <c>l</c> and <c>r</c>, yielding a new <see cref="T:Kernel.Arithmetic.Integer"/> 
        /// </summary>
        /// <param name="l">L.</param>
        /// <param name="r">The red component.</param>
        /// <returns>The <see cref="T:Kernel.Arithmetic.Integer"/> that is the <c>l</c> % <c>r</c>.</returns>
        public static Integer operator %(Integer l, Integer r)
        => new Integer(l.data % r.data);

        /// <summary>
        /// Determines whether one specified <see cref="Kernel.Arithmetic.Integer"/> is greater than another specfied <see cref="Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> is greater than <c>r</c>; otherwise, <c>false</c>.</returns>
        public static bool operator >(Integer l, Integer r) => l.data > r.data;

        /// <summary>
        /// Determines whether one specified <see cref="Kernel.Arithmetic.Integer"/> is lower than another specfied <see cref="Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> is lower than <c>r</c>; otherwise, <c>false</c>.</returns>
        public static bool operator <(Integer l, Integer r) => l.data < r.data;

        /// <summary>
        /// Determines whether one specified <see cref="Kernel.Arithmetic.Integer"/> is greater than or equal to another
        /// specfied <see cref="Kernel.Arithmetic.Integer"/>
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> is greater than or equal to <c>r</c>; otherwise, <c>false</c>.</returns>
        public static bool operator >=(Integer l, Integer r) => l.data >= r.data;
       
        /// <summary>
        /// Determines whether one specified <see cref="Kernel.Arithmetic.Integer"/> is lower than or equal to another
        /// specfied <see cref="Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> is lower than or equal to <c>r</c>; otherwise, <c>false</c>.</returns>
        public static bool operator <=(Integer l, Integer r) => l.data <= r.data;

        /// <summary>
        /// Determines whether a specified instance of <see cref="Kernel.Arithmetic.Integer"/> is equal to another
        /// specified <see cref="Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> and <c>r</c> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Integer l, Integer r) => l.data == r.data;

        /// <summary>
        /// Determines whether a specified instance of <see cref="Kernel.Arithmetic.Integer"/> is not equal to another
        /// specified <see cref="Kernel.Arithmetic.Integer"/>.
        /// </summary>
        /// <param name="l">The first <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <param name="r">The second <see cref="Kernel.Arithmetic.Integer"/> to compare.</param>
        /// <returns><c>true</c> if <c>l</c> and <c>r</c> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Integer l, Integer r) => l.data != r.data;

        public override int GetHashCode() => data.GetHashCode();

        public override bool Equals(object obj) => obj is Integer a && a.data == this.data;

        public static implicit operator Rational(Integer @int) => new Rational(@int.data);
        public static implicit operator BigInteger(Integer @int) => @int.data;
        public static implicit operator Integer(int number) => new Integer(number);
        public static implicit operator int(Integer @int)
        => @int.data > int.MaxValue ?
                throw new System.InvalidCastException("Value is bigger than max size")
            : (int)@int.data;
    }
}
