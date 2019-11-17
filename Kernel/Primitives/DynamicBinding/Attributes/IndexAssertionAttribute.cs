using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Primitives.DynamicBinding.BindingVariables;
namespace Kernel.Primitives.DynamicBinding.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public abstract class IndexAssertionAttribute : AssertionAttribute
    {
        protected readonly Expression expression;

        protected IndexAssertionAttribute(Expression condition, string errorMessage, int index, bool negated, bool optional = false)
            : base(errorMessage)
        {
            Negated = negated;
            Index = index;
            expression = condition;
            Optional = optional;
        }

        public override Expression Expression => Negated ? Not(expression) : expression;
        public static Expression ElementAt(int index) => Property(ListParameter, "Item", Constant(index));
        public Expression Element => ElementAt(Index);

        public int Index { get; }
        public bool Negated { get; }
        public bool Optional { get; }
    }

}
