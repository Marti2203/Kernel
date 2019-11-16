using System;
using System.Linq.Expressions;
using Kernel.BaseTypes;
using static System.Linq.Expressions.Expression;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public abstract class AssertionAttribute : Attribute
    {
        protected AssertionAttribute(string errorMessage) => ErrorMessage = errorMessage;
        public abstract Expression Expression { get; }
        public string ErrorMessage { get; }
    }

}
