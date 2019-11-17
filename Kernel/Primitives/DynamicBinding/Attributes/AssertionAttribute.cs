using System;
using System.Linq.Expressions;
namespace Kernel.Primitives.DynamicBinding.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public abstract class AssertionAttribute : Attribute
    {
        protected AssertionAttribute(string errorMessage) => ErrorMessage = errorMessage;
        public abstract Expression Expression { get; }
        public string ErrorMessage { get; }
    }

}
