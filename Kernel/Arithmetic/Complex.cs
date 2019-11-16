using Kernel.BaseTypes;
using System.Collections.Generic;
using InvalidOperationException = System.InvalidOperationException;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Complex number, consisting of a real and imaginary part.
    /// Highest number in hierarchy for now.
    /// </summary>
    public sealed class Complex : Number
    {
        static readonly Dictionary<(Number, Number), Complex> cache = new Dictionary<(Number, Number), Complex>();

        public override NumberHierarchy Priority => NumberHierarchy.Complex;

        public new bool Exact => RealPart.Exact && Imaginary.Exact;

        public Number RealPart { get; }
        public Number Imaginary { get; }

        Complex(Number real, Number imaginary)
        {
            RealPart = real;
            Imaginary = imaginary;
        }

        public static Complex Get(Number real, Number imaginary)
        {
            var key = (real, imaginary);
            if (cache.ContainsKey(key)) return cache[key];
            cache.Add(key, new Complex(real, imaginary));
            return cache[key];
        }

        public static Complex GetPolar(Number magnitude, Number angle)
        {
            return null;
        }

        public static Complex GetRectangle(Number real, Number imag) => Get(real, imag);

        public override string ToString()
        => Imaginary.Equals(0) ? RealPart.ToString() : $"{RealPart}{ (Imaginary >= 0 ? "+" : "-") }{Imaginary}i";

        protected override Number Add(Number num)
        {
            Complex other = Convert(num);
            return Get(RealPart + other.RealPart, Imaginary + other.Imaginary);
        }

        protected override Number Subtract(Number num)
        {
            Complex other = Convert(num);
            return Get(RealPart - other.RealPart, Imaginary - other.Imaginary);
        }

        protected override Number SubtractFrom(Number num)
        {
            Complex other = Convert(num);
            return Get(other.RealPart - RealPart, other.Imaginary - Imaginary);
        }

        protected override Number Multiply(Number num)
        {
            Complex other = Convert(num);
            return Get(other.RealPart * RealPart - other.Imaginary * Imaginary, other.RealPart * Imaginary + other.Imaginary * RealPart);
        }

        protected override Number Divide(Number num)
        {
            Complex other = Convert(num);
            Complex numerator = other.Multiply(Conjugate(this)) as Complex;
            Number denominator = Absolute(this);
            return Get(numerator.RealPart / denominator, numerator.Imaginary / denominator);
        }

        protected override Number DivideBy(Number num)
        {
            Complex other = Convert(num);
            Complex numerator = Multiply(Conjugate(other)) as Complex;
            Number denominator = Absolute(other);
            return Get(numerator.RealPart / denominator, numerator.Imaginary / denominator);
        }

        protected override Number Negate() => Get(-RealPart, -Imaginary);

        public static Number Absolute(Complex complex)
        => complex.RealPart * complex.RealPart + complex.Imaginary * complex.Imaginary;

        public static Complex Conjugate(Complex number) => Get(number.RealPart, -number.Imaginary);

        protected override Boolean LessThan(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean BiggerThan(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean LessThanOrEqual(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean BiggerThanOrEqual(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override int Compare(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        static Complex Convert(Number number)
        {
            switch (number)
            {
                case Integer integer:
                    return Get(integer, Integer.Zero);
                case Rational rational:
                    return Get(rational, Rational.Get(0));
                case Real real:
                    return Get(real, Real.Get(0m));
                case Complex complex:
                    return complex;
            }
            throw new InvalidOperationException("WATAFAK?!");
        }
    }
}