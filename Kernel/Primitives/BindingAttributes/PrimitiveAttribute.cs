using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PrimitiveAttribute : Attribute
    {
        public string PrimitiveName { get; }
        public int InputCount { get; }
        public bool Variadic { get; }

        public PrimitiveAttribute(string primitiveName, int inputCount = 0, bool variadic = false)
        {
            PrimitiveName = primitiveName;
            InputCount = inputCount;
            Variadic = variadic;
        }

        public Expression[] Parameters(ParameterExpression input) => Enumerable
            .Range(0, InputCount)
            .Select<int, Expression>(x => Property(input, "Item", Constant(x)))
            .ToArray();
    }

}
