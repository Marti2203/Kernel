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
		public static MethodInfo EnumerableGeneric(Type t, string name)
		=> typeof(Enumerable)
				.GetMethods()
			.First(method => method.Name == name)
			.MakeGenericMethod(t);

		public static Expression CallEnumerable(Type type, string name, params Expression[] arguments)
		=> Call(null, EnumerableGeneric(type, name), arguments);

		public static Expression ChainEnumerable(Type type, string[] methods, params Expression[] arguments)
		{

			Expression current = CallEnumerable(type, methods[0], arguments);
			for (int i = 1; i < methods.Length; i++)
				current = CallEnumerable(type, methods[i], current);
			return current;
		}

		public static MethodInfo EnumerableGeneric<T>(string name) => EnumerableGeneric(typeof(T), name);

		public static Expression CallEnumerable<T>(string name, params Expression[] arguments)
		=> CallEnumerable(typeof(T), name, arguments);

		public static Expression ChainEnumerable<T>(string[] methods, params Expression[] arguments)
		=> ChainEnumerable(typeof(T), methods, arguments);

		public static Expression Throw(Expression check, string errorMessage)
		=> IfThen(check, Expression.Throw(Constant(new ArgumentException(errorMessage))));

	}
}
