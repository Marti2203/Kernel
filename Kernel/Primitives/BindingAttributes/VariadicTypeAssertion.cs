using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class VariadicTypeAssertion : AssertionAttribute
    {
        public VariadicTypeAssertion(Type type, int skip = 0)
            : base($"All { (skip > 0 ? $"after {skip}" : "")} arguments must be {type.Name}")
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
                var input = Skip == 0 ? InputCasted : CallFunction("Skip", InputCasted, Constant(Skip));
                return Not(CallFunction("All", input, TypePredicate()));
            }
        }
        public Type Type { get; }
        public int Skip { get; }
    }

}
