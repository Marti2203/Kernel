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
using ExpressionList = System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression>;
using static Kernel.Primitives.DynamicBinding.BindingVariables;
using Console = System.Console;
using Kernel.Primitives.DynamicBinding.Attributes;

namespace Kernel.Primitives.DynamicBinding
{
    public static class DynamicFunctionBinding
    {
        static Expression GenerateParameterCountCheck(PrimitiveAttribute primitive
                                                    , IEnumerable<TypeAssertionAttribute> typeAssertions)
        {
            var expectedCount = Constant(primitive.InputCount);
            var optionalParameters = typeAssertions.Count(x => x.Optional);
            Expression countPredicate = null;
            var difference = "";

            if (optionalParameters == 0)
            {
                if (primitive.InputCount != 0)
                {
                    countPredicate = LessThan(ArgumentCount, expectedCount);
                    difference = "Not enough";
                }
                if (!primitive.Variadic)
                {
                    var compareInputWithExpected = NotEqual(ArgumentCount, expectedCount);
                    if (countPredicate == null)
                    {
                        difference = "Too many";
                        countPredicate = compareInputWithExpected;
                    }
                    else
                    {
                        difference += " or too many";
                        countPredicate = Or(countPredicate, compareInputWithExpected);
                    }
                }
            }
            else
            {
                difference = "Not enough or too many";
                countPredicate = Or(GreaterThan(ArgumentCount, expectedCount)
                              , LessThan(ArgumentCount, Constant(primitive.InputCount - optionalParameters)));
            }

            return countPredicate == null
                ? Empty()
                : (Expression)Block(
#if DebugCallMethods
                         Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant($"The function {primitive.PrimitiveName} has input count {primitive.InputCount} and receives ")),
                         Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(int) }), realCount),
                         Call(null, typeof(Console).GetMethod("Write", new[] { typeof(string) }), Constant("With Input ")),
                         Call(null, typeof(Console).GetMethod("WriteLine", new[] { typeof(object) }), Input),
#endif
                         ThrowIF(countPredicate, $"{difference} arguments for combiner '{primitive.PrimitiveName}'"));
        }
        static ExpressionList GenerateMethodCallParameters(IEnumerable<TypeAssertionAttribute> typeAssertions,
                                                                    PrimitiveAttribute primitiveInformation)
        {
            IEnumerable<Expression> Parameters(ParameterExpression input) => Enumerable
            .Range(0, primitiveInformation.InputCount)
            .Select(x => Property(input, "Item", Constant(x)));

            List<Expression> GenerateCallParameterCasts(ExpressionList parameters)
            => parameters.Select((expression, index) =>
            {
                if (!(typeAssertions.FirstOrDefault(x => x.Index == index) is TypeAssertionAttribute assertion))
                {
                    return expression;
                }
                else
                {
                    var cast = TypeAs(expression, assertion.Type);
                    if (assertion.Optional)
                    {
                        var nullCast = TypeAs(Constant(null), assertion.Type);
                        var hasEnoughArguments = GreaterThan(ArgumentCount, Constant(index));
                        return Condition(hasEnoughArguments, cast, nullCast);
                    }
                    else
                    {
                        return cast;
                    }
                }
            }).ToList();

            if (typeAssertions.Count() > primitiveInformation.InputCount)
                throw new System.InvalidOperationException("Input cannot be less than type assertions");

            var methodCallParameters = GenerateCallParameterCasts(Parameters(ListParameter));
            if (primitiveInformation.Variadic)
            {
                Expression restOfParameters = methodCallParameters.Any()
                                            ? CallFunction("Skip", ListParameter, Constant(primitiveInformation.InputCount))
                                            : ListParameter;

                methodCallParameters.Add(restOfParameters);
            }
            return methodCallParameters;
        }
        public static System.Func<List, Object> CreateBinding(MethodInfo method)
        {
            var primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();

            var assertions = method.GetCustomAttributes<AssertionAttribute>();

            var typeAssertions = method.GetCustomAttributes<TypeAssertionAttribute>();

            if (primitiveInformation.Variadic && typeAssertions.Any(assertion => assertion.Optional))
            {
                throw new System.InvalidOperationException("Cannot have a variadic function with an optional argument.");
            }

            var assignList = Assign(ListParameter, InputCasted);

            var assignArgumentCount = Assign(ArgumentCount, CallFunction("Count", ListParameter, Constant(false)));

            var methodCallParameters = GenerateMethodCallParameters(typeAssertions, primitiveInformation);

            var methodCall = Call(null, method, methodCallParameters);

            var countCheck = GenerateParameterCountCheck(primitiveInformation, typeAssertions);

            var throws = assertions.Any() ? Block(assertions.Select(x =>
            {
                var baseThrow = ThrowIF(x.Expression, x.ErrorMessage);
                return x is IndexAssertionAttribute indexAssertion && indexAssertion.Optional
                    ? IfThen(GreaterThan(ArgumentCount, Constant(indexAssertion.Index)), baseThrow)
                    : baseThrow;
            })) as Expression : Empty();

            var parameters = new[] { ListParameter, ArgumentCount };

            var expressions = new[] { assignList, assignArgumentCount, countCheck, throws, methodCall };

            var body = Block(parameters, expressions);

#if DebugMethods
            if (method.Name == "Display")
            {
                Console.WriteLine(method.Name);
                var expressions = body
                    .Expressions
                    .SelectMany(expression => expression is BlockExpression block ? block.Expressions.ToArray() : new[] { expression });
                Console.WriteLine(string.Join("\n", expressions));
                Console.WriteLine();
            }
#endif
            return Lambda(body, true, Input).Compile() as System.Func<List, Object>;
        }

    }
}
