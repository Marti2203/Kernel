using System;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class TypeAssertionAttribute : IndexAssertionAttribute
    {
        public Type Type { get; private set; }

        public TypeAssertionAttribute(int index, Type type)
            : base(ElementAtIs(index, type), $"{index} Argument is not a {type}", index, true)
        {
            Type = type;
        }
        public TypeAssertionAttribute(int index, Type type1, Type type2)
            : base(OrElse(ElementAtIs(index, type1), ElementAtIs(index, type2)),
                   $"{index} Argument is not a {type1} or {type2}", index, true) => Type = typeof(Object);

        static TypeBinaryExpression ElementAtIs(int index, Type type) => TypeIs(ElementAt(index), type);
    }

}
