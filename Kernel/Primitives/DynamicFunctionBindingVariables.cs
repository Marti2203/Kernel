#define DebugMethods
//#define DebugCallMethods
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using Kernel.BaseTypes;
namespace Kernel.Primitives
{
    public static class DynamicFunctionBindingVariables
    {
        public static readonly ParameterExpression ListParameter = Parameter(typeof(List), "list");
        public static readonly ParameterExpression ArgumentCount = Parameter(typeof(int), "argumentCount");
        public static readonly ParameterExpression Input = Parameter(typeof(Object), "@object");
        public static readonly UnaryExpression InputCasted = TypeAs(Input, typeof(List));

    }
}
