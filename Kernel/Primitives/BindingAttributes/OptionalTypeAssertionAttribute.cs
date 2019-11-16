using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class OptionalTypeAssertionAttribute : IndexAssertionAttribute
    {
        public OptionalTypeAssertionAttribute(int index,Type type):
            base(Empty(), $"{index} Argument is an optional argument that is not of type {type}", index, true)
        {

        }

        //static Expression HasSize(int s) => If CallFunction("Count",InputCasted)
    }
}