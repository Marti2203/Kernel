using System;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class PredicateAssertionAttribute : IndexAssertionAttribute
    {
        public PredicateAssertionAttribute(int index, Type type, string methodName, bool negated = true, bool optional = false)
            : base(StaticCallOnElementAt(type.GetMethod(methodName, new[] { typeof(Object) }), index)
                   , $"{index} Argument is not valid by the predicate {type.Name}.{methodName}", index, negated, optional)
        { }


        static Expression StaticCallOnElementAt(MethodInfo predicate, int index)
        => Call(null, predicate, ElementAt(index));
    }

}
