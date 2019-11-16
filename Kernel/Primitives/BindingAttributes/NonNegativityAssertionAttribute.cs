using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class NonNegativityAssertionAttribute : IndexAssertionAttribute
    {
        public NonNegativityAssertionAttribute(int index)
            : base(And
                    (
                     ElementIsInteger(index),
                       GreaterThanOrEqual(TypeAs(ElementAt(index), typeof(Arithmetic.Integer)),
                                          Constant(Arithmetic.Integer.Zero))
                    )
                  , $"{index} Argument is a negative integer", index, true)
        {
        }
        static TypeBinaryExpression ElementIsInteger(int index) => TypeIs(ElementAt(index), typeof(Arithmetic.Integer));
    }

}
