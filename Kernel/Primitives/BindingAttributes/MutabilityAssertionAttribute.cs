using System;
using static System.Linq.Expressions.Expression;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MutabilityAssertionAttribute : IndexAssertionAttribute
    {
        public MutabilityAssertionAttribute(int index, bool required = true)
            : base(Property(ElementAt(index), "Mutable"), $"{index} Argument is not mutable", index, !required) { }
    }

}
