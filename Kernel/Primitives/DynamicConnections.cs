//#define DebugMethods
//#define DebugCallMethods
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using System.Reflection;

namespace Kernel.Primitives
{
	public static class DynamicConnections
	{
		static IEnumerable<Expression> GenerateCallParameters(IEnumerable<TypeAssertionAttribute> typeAssertions
								   , IEnumerable<Expression> parameters)
		=> parameters.Select((expression, index) =>
		{
			TypeAssertionAttribute assertion = typeAssertions.FirstOrDefault(x => x.Index == index);
			return assertion == null ? expression : TypeAs(expression, assertion.Type);
		});

		static Expression GenerateCountCheck(PrimitiveAttribute primitive)
		{
			var realCount = Parameter(typeof(int), "realCount");
			var inputCasted = TypeAs(AssertionAttribute.Input, typeof(IEnumerable<Object>));
			var count = CallEnumerable<Object>("Count", inputCasted); // In case of Null sent
			var expectedCount = Constant(primitive.InputCount);
			Expression predicate = null;
			if (primitive.InputCount != 0)
			{
				predicate = LessThan(realCount, expectedCount);
			}
			if (!primitive.Variadic)
			{
				var compareInputWithExpected = NotEqual(realCount, expectedCount);
				predicate = predicate == null ? compareInputWithExpected : Or(predicate, compareInputWithExpected);
			}
			if (predicate == null)
				return Empty();
			return Block(new[] { realCount },
						 Assign(realCount, count),
#if DebugCallMethods
						 Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant($"The function {primitive.PrimitiveName} has input count {primitive.InputCount} and receives ")),
						 Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }), realCount),
						 Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant("With Input ")),
						 Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(object) }), AssertionAttribute.InputCasted),
#endif
						 Throw(predicate, "Not enough or too many arguments for combiner"));
		}

		public static Func<Object, Object> CreatePipeFunction(MethodInfo method)
		{
			var primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
			var assertions = method.GetCustomAttributes<AssertionAttribute>();
			var typeCompilance = method.GetCustomAttribute<TypeCompilanceAssertionAttribute>();
			var typeAssertions = method.GetCustomAttributes<TypeAssertionAttribute>();

			var list = Parameter(typeof(List), "list");
			var assignment = Assign(list, AssertionAttribute.InputCasted);
			var methodCallParameters = GenerateMethodCallParameters(typeAssertions,
																	primitiveInformation,
																	typeCompilance,
																	list);

			Expression methodCall = Call(null, method, methodCallParameters);


			Expression countCheck = GenerateCountCheck(primitiveInformation);
			var body = Block(new[] { list },
							 assignment,
							 countCheck,
							 Block(assertions.Select(x => Throw(x.Expression, x.ErrorMessage))),
							 methodCall);

#if DebugMethods
			// if (method.Name == "MakeEnvironment")
			//{
				Console.WriteLine($"Name: {method.Name}");
				Console.WriteLine($"Body Expressions");
				var expressions = body
					.Expressions
					.SelectMany(expression => expression is BlockExpression block ? block.Expressions.ToArray() : new[] { expression });
				Console.WriteLine(string.Join("\n", expressions));
				Console.WriteLine();
			//}
#endif

			var result = Lambda(body, true, AssertionAttribute.Input).Compile();
			return result as Func<Object, Object>;
		}

		static IEnumerable<Expression> GenerateMethodCallParameters(IEnumerable<TypeAssertionAttribute> typeAssertions,
																	PrimitiveAttribute primitiveInformation,
																	TypeCompilanceAssertionAttribute typeCompilance,
																	ParameterExpression input)
		{
			if (typeAssertions.Count() > primitiveInformation.InputCount)
				throw new InvalidOperationException("Input cannot be less than type assertions");

			var methodCallParameters = Enumerable.Empty<Expression>();
			if (primitiveInformation.InputCount != 0)
			{
				methodCallParameters = GenerateCallParameters(typeAssertions, primitiveInformation.Parameters(input));
			}
			if (primitiveInformation.Variadic)
			{
				Expression restOfParameters = input;
				if (methodCallParameters.Any())
				{

					if (typeCompilance != null)
						restOfParameters = CallEnumerable(typeCompilance.Type
									   , "ToArray",
						CallEnumerable(typeCompilance.Type
									   , "Cast"
									   , CallEnumerable<Object>(
										   "Skip",
										   input,
										   Constant(typeCompilance.Skip))));
					else restOfParameters = CallEnumerable<Object>("ToArray",
																   CallEnumerable<Object>("Skip",
																						  input,
																						  Constant(methodCallParameters.Count())));
				}
				else if (typeCompilance != null)
				{
					string[] skipMethods = { "Cast", "ToArray" };
					restOfParameters = ChainEnumerable(typeCompilance.Type, skipMethods, input);
				}

				methodCallParameters = methodCallParameters.Concat(new[] { restOfParameters });
			}

			return methodCallParameters;
		}

	}
	public static class PredicateApplicative<T>
	{
		public static Combiners.Applicative Instance => new Combiners.Applicative(Validate, Name);
		static string Name => typeof(T).Name.ToLower() + "?";
		static Boolean Validate(Object @object)
		{
			List list = @object as List;
			if (!list.Any()) return Boolean.True;
			return (Boolean)list.All(x => x is T);
		}
	}
}
