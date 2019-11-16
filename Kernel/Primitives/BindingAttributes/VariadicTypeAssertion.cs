using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using static Kernel.Primitives.DynamicFunctionBindingVariables;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class VariadicTypeAssertion : AssertionAttribute
    {
        public VariadicTypeAssertion(Type type, int skip = 0)
            : base($"All arguments { (skip > 0 ? $"after {skip}" : "")} must be of type {type.Name}")
        {
            Skip = skip;
            Type = type;
        }

        public LambdaExpression TypePredicate()
        {
            ParameterExpression x = Parameter(typeof(Object), "x");
            return Lambda(TypeIs(x, Type), x);
        }

        public override Expression Expression
        {
            get
            {
                var input = Skip == 0 ? ListParameter : CallFunction("Skip", ListParameter, Constant(Skip));
                return Not(CallFunction("All", input, TypePredicate()));
            }
        }
        public Type Type { get; }
        public int Skip { get; }
    }

}
