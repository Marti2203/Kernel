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

        public new bool Exact => RealPart.Exact && ImaginaryPart.Exact;

        public Number RealPart { get; }
        public Number ImaginaryPart { get; }

        Complex(Number real, Number imaginary)
        {
            RealPart = real;
            ImaginaryPart = imaginary;
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
        => ImaginaryPart.Equals(0) ? RealPart.ToString() : $"{RealPart}{ (ImaginaryPart >= 0 ? "+" : "-") }{ImaginaryPart}i";

        protected override Number Add(Number num)
        {
            Complex other = Convert(num);
            return Get(RealPart + other.RealPart, ImaginaryPart + other.ImaginaryPart);
        }

        protected override Number Subtract(Number num)
        {
            Complex other = Convert(num);
            return Get(RealPart - other.RealPart, ImaginaryPart - other.ImaginaryPart);
        }

        protected override Number SubtractFrom(Number num)
        {
            Complex other = Convert(num);
            return Get(other.RealPart - RealPart, other.ImaginaryPart - ImaginaryPart);
        }

        protected override Number Multiply(Number num)
        {
            Complex other = Convert(num);
            return Get(other.RealPart * RealPart - other.ImaginaryPart * ImaginaryPart, other.RealPart * ImaginaryPart + other.ImaginaryPart * RealPart);
        }

        protected override Number Divide(Number num)
        {
            Complex other = Convert(num);
            Complex numerator = other.Multiply(Conjugate(this)) as Complex;
            Number denominator = Absolute(this);
            return Get(numerator.RealPart / denominator, numerator.ImaginaryPart / denominator);
        }

        protected override Number DivideBy(Number num)
        {
            Complex other = Convert(num);
            Complex numerator = Multiply(Conjugate(other)) as Complex;
            Number denominator = Absolute(other);
            return Get(numerator.RealPart / denominator, numerator.ImaginaryPart / denominator);
        }

        protected override Number Negate() => Get(-RealPart, -ImaginaryPart);

        public static Number Absolute(Complex complex)
        => complex.RealPart * complex.RealPart + complex.ImaginaryPart * complex.ImaginaryPart;

        public static Complex Conjugate(Complex number) => Get(number.RealPart, -number.ImaginaryPart);

        protected override Boolean LessThan(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean BiggerThan(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean LessThanOrEqual(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override Boolean BiggerThanOrEqual(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        protected override int Compare(Number num) => throw new InvalidOperationException("Cannot compare Complex Numbers directly.");

        static Complex Convert(Number number) => number switch
        {
            Integer integer => Get(integer, Integer.Zero),
            Rational rational => Get(rational, Rational.Get(0)),
            Real real => Get(real, Real.Get(0m)),
            Complex complex => complex,
            _ => throw new InvalidOperationException("WATAFAK?!"),
        };

        protected override bool InternalEquals(Object other)
        {
            var compOther = other as Complex;
            return compOther.RealPart == RealPart && compOther.ImaginaryPart == ImaginaryPart;
        }
    }
}