using Kernel.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;
namespace Kernel.Utilities
{
    public static class MethodCallUtilities
    {
        public static MethodInfo Function(string name, int argumentCount)
        {
            var result = typeof(ListHelper)
                .GetMethods()
                .First(method => method.Name == name && method.GetParameters().Length == argumentCount);
            if (result.IsGenericMethod)
                return result.MakeGenericMethod(typeof(Object));
            return result;
        }

        public static Expression CallFunction(string name, params Expression[] arguments)
        => Call(null, Function(name, arguments.Length), arguments);

        public static Expression ChainFunctions(string[] methods, params Expression[] arguments)
        {
            Expression currentValue = CallFunction(methods[0], arguments);
            for (int i = 1; i < methods.Length; i++)
                currentValue = CallFunction(methods[i], currentValue);
            return currentValue;
        }

        public static Expression Throw(Expression check, string errorMessage)
        => IfThen(check, Expression.Throw(Constant(new ArgumentException(errorMessage))));

    }
}
