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
        static readonly Dictionary<(double, double), Complex> cache = new Dictionary<(double, double), Complex>();

        public override NumberHierarchy Priority => NumberHierarchy.Complex;

        public readonly double real;
        public readonly double imaginary;

        Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public static Complex Get(double real, double imaginary)
        {
            var key = (real, imaginary);
            if (cache.ContainsKey(key)) return cache[key];
            cache.Add(key, new Complex(real, imaginary));
            return cache[key];
        }

        public static Complex Get(string real, string imaginary)
        => Get(double.Parse(real), double.Parse(imaginary));

        public override string ToString()
        => Math.Abs(imaginary) < double.Epsilon ? real.ToString() : $"{real}{ (imaginary > 0 ? "+" : "-") }{imaginary}i";

        public override bool Equals(Object other)
        {
            if (!(other is Number n)) return false;
            if (n.Exact != Exact) return false;
            Complex complex = (Complex)other;
            return Math.Abs(real - complex.real) < double.Epsilon && Math.Abs(imaginary - complex.imaginary) < double.Epsilon;
        }

        protected override Number Add(Number num)
        {
            Complex other = num as Complex;
            return Get(real + other.real, imaginary + other.imaginary);
        }

        protected override Number Subtract(Number num)
        {
            Complex other = num as Complex;
            return Get(real - other.real, imaginary - other.imaginary);
        }

        protected override Number SubtractFrom(Number num)
        {
            Complex other = num as Complex;
            return Get(other.real - real, other.imaginary - imaginary);
        }

        protected override Number Multiply(Number num)
        {
            Complex other = num as Complex;

            return Get(other.real * real - other.imaginary * imaginary, other.real * imaginary + other.imaginary * real);
        }

        protected override Number Divide(Number num)
        {
            Complex other = num as Complex;
            Complex numerator = Multiply(Conjugate(other)) as Complex;
            double denominator = other.real * other.real + other.imaginary + other.imaginary;
            return Get(numerator.real / denominator, numerator.imaginary / denominator);
        }

        public static Complex Conjugate(Complex number) => Get(number.real, -number.imaginary);

        protected override Number DivideBy(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Number Negate()
        {
            throw new NotImplementedException();
        }

        protected override Boolean LessThan(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Boolean BiggerThan(Number num)
        {
            throw new NotImplementedException();
        }

        protected override Boolean LessThanOrEqual(Number number)
        {
            throw new NotImplementedException();
        }

        protected override Boolean BiggerThanOrEqual(Number number)
        {
            throw new NotImplementedException();
        }


        protected override int Compare(Number number)
        {
            throw new NotImplementedException();
        }

    }
}