//#define DebugMethods
//#define DebugCallMethods
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using System.Reflection;
using Kernel.Utilities;
using Kernel.BaseTypes;
using Kernel.Primitives.BindingAttributes;

namespace Kernel.Primitives
{
    public static class DynamicFunctionBinding
    {
        static IEnumerable<Expression> GenerateCallParameterCasts(IEnumerable<TypeAssertionAttribute> typeAssertions
                                   , IEnumerable<Expression> parameters)
        => parameters.Select((expression, index) =>
        {
            TypeAssertionAttribute assertion = typeAssertions.FirstOrDefault(x => x.Index == index);
            return assertion == null ? expression : TypeAs(expression, assertion.Type);
        });

        static Expression GenerateParameterCountCheck(PrimitiveAttribute primitive)
        {
            var realCount = Parameter(typeof(int), "realCount");
            var count = CallFunction("Count", AssertionAttribute.InputCasted, Constant(false));
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
            return predicate == null
                ? Empty()
                : (Expression)Block(new[] { realCount },
                         Assign(realCount, count),
#if DebugCallMethods
                         Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant($"The function {primitive.PrimitiveName} has input count {primitive.InputCount} and receives ")),
                         Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }), realCount),
                         Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant("With Input ")),
                         Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(object) }), AssertionAttribute.InputCasted),
#endif
                         Throw(predicate, $"Not enough or too many arguments for combiner {primitive.PrimitiveName}"));
        }

        public static System.Func<List, Object> CreateBinding(MethodInfo method)
        {
            var primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
            var assertions = method.GetCustomAttributes<AssertionAttribute>();
            var typeCompilance = method.GetCustomAttribute<VariadicTypeAssertion>();
            var typeAssertions = method.GetCustomAttributes<TypeAssertionAttribute>();

            var list = Parameter(typeof(List), "list");
            var assignment = Assign(list, AssertionAttribute.InputCasted);
            var methodCallParameters = GenerateMethodCallParameters(typeAssertions,
                                                                    primitiveInformation,
                                                                    list);

            Expression methodCall = Call(null, method, methodCallParameters);


            Expression countCheck = GenerateParameterCountCheck(primitiveInformation);
            Expression throws = assertions.Any() ? Block(assertions.Select(x => Throw(x.Expression, x.ErrorMessage))) as Expression : Empty();
            var body = Block(new[] { list }, assignment, countCheck, throws, methodCall);

#if DebugMethods
            if (method.Name != null)
            {
                Console.WriteLine($"Name: {method.Name}");
                Console.WriteLine($"Body Expressions");
                var expressions = body
                    .Expressions
                    .SelectMany(expression => expression is BlockExpression block ? block.Expressions.ToArray() : new[] { expression });
                Console.WriteLine(string.Join("\n", expressions));
                Console.WriteLine();
            }
#endif

            return Lambda(body, true, AssertionAttribute.Input).Compile() as System.Func<List, Object>;
        }

        static IEnumerable<Expression> GenerateMethodCallParameters(IEnumerable<TypeAssertionAttribute> typeAssertions,
                                                                    PrimitiveAttribute primitiveInformation,
                                                                    ParameterExpression list)
        {
            if (typeAssertions.Count() > primitiveInformation.InputCount)
                throw new System.InvalidOperationException("Input cannot be less than type assertions");

            var methodCallParameters = Enumerable.Empty<Expression>();
            if (primitiveInformation.InputCount != 0)
            {
                methodCallParameters = GenerateCallParameterCasts(typeAssertions, primitiveInformation.Parameters(list));
            }
            if (primitiveInformation.Variadic)
            {
                Expression restOfParameters = list;
                if (methodCallParameters.Any())
                {
                    var skipCount = primitiveInformation.InputCount;
                    restOfParameters = CallFunction("Skip", list, Constant(skipCount));
                }

                methodCallParameters = methodCallParameters.Concat(new[] { restOfParameters });
            }

            return methodCallParameters;
        }

    }
    public static class PredicateApplicative<T> where T : Object
    {
        public static Combiners.Applicative Instance => new Combiners.Applicative(Validate, Name);
        static string Name => typeof(T).Name.ToLower() + "?";
        static Boolean Validate(Object @object)
        {
            if (!(@object is List list))
                throw new System.ArgumentException("Validate only accepts a list of objects", nameof(@object));
            return list.All<T>(x => x is T);
        }
    }
}
