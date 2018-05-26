using System;
namespace Kernel.Arithmetic
{
    /// <summary>
    /// Complex number, consisting of a real and imaginary part.
    /// Highest number in hierarchy
    /// </summary>
    public sealed class Complex : Number
    {
        /// <summary>
        /// The real part.
        /// </summary>
        public readonly decimal real;
        /// <summary>
        /// The imaginary part.
        /// </summary>
        public readonly decimal imaginary;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kernel.Arithmetic.Complex"/> class.
        /// </summary>
        /// <param name="real">Real part.</param>
        /// <param name="imaginary">Imaginary part.</param>
        public Complex(decimal real,decimal imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Complex"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Kernel.Arithmetic.Complex"/>.</returns>
        public override string ToString() => $"{real}{ (imaginary > 0 ? "+" : "") }{imaginary}i";
    }
}
