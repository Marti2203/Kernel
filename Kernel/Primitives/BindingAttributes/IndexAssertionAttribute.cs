using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class IndexAssertionAttribute : AssertionAttribute
    {
        protected readonly Expression expression;

        protected IndexAssertionAttribute(Expression condition, string errorMessage, int index, bool negated)
            : base(errorMessage)
        {
            Negated = negated;
            Index = index;
            expression = condition;
        }

        public override Expression Expression => Negated ? Not(expression) : expression;

        public static Expression ElementAt(int index) => Property(InputCasted, "Item", Constant(index));

        public Expression Element => ElementAt(Index);

        public int Index { get; }
        public bool Negated { get; }
    }

}
