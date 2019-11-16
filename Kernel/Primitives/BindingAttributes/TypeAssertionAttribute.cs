using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class TypeAssertionAttribute : IndexAssertionAttribute
    {
        public Type Type { get; private set; }

        public TypeAssertionAttribute(int index, Type type, bool optional = false)
            : base(ElementAtIs(index, type), $"{index} Argument is not a/an {type}", index, true, optional)
        {
            Type = type;
        }
        public TypeAssertionAttribute(int index, Type type1, Type type2, bool optional = false)
            : base(OrElse(ElementAtIs(index, type1), ElementAtIs(index, type2)),
                   $"{index} Argument is not {type1} nor {type2}", index, true, optional) => Type = typeof(Object);
        public TypeAssertionAttribute(int index, bool optional = false, params Type[] types)
            : base(types.Select(t => ElementAtIs(index, t) as Expression).Aggregate((curr, next) => OrElse(curr, next)),
                   $"{index} Argument does not have a type of the set [{string.Join<Type>(",", types)}]", index, true, optional) => Type = typeof(Object);

        static TypeBinaryExpression ElementAtIs(int index, Type type) => TypeIs(ElementAt(index), type);
    }

}
