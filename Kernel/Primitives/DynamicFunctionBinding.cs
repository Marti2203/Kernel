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
using ExpressionList = System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression>;
using Console = System.Console;
using static Kernel.Primitives.DynamicFunctionBindingVariables;
namespace Kernel.Primitives
{
    public static class DynamicFunctionBinding
    {

        //static Expression HasEnough

        static ExpressionList GenerateCallParameterCasts(IEnumerable<TypeAssertionAttribute> typeAssertions, ExpressionList parameters)
        => parameters.Select((expression, index) =>
        {
            TypeAssertionAttribute assertion = typeAssertions.FirstOrDefault(x => x.Index == index);
            return assertion == null ?
                               expression :
                               assertion.Optional ?
                               Condition(GreaterThan(ArgumentCount, Constant(index)), TypeAs(expression, assertion.Type), TypeAs(Constant(null), assertion.Type)) :
                               TypeAs(expression, assertion.Type) as Expression;
        });

        static Expression GenerateParameterCountCheck(PrimitiveAttribute primitive, IEnumerable<TypeAssertionAttribute> typeAssertions, Expression realCount)
        {
            var expectedCount = Constant(primitive.InputCount);
            var optionalParameters = typeAssertions.Count(x => x.Optional);
            Expression countPredicate = null;
            var difference = "";

            if (optionalParameters == 0)
            {
                if (primitive.InputCount != 0)
                {
                    countPredicate = LessThan(realCount, expectedCount);
                    difference = "Not enough";
                }
                if (!primitive.Variadic)
                {
                    var compareInputWithExpected = NotEqual(realCount, expectedCount);
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
                countPredicate = Or(GreaterThan(realCount, expectedCount)
                              , LessThan(realCount, Constant(primitive.InputCount - optionalParameters)));
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

        public static System.Func<List, Object> CreateBinding(MethodInfo method)
        {
            var primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
            var assertions = method.GetCustomAttributes<AssertionAttribute>();
            var typeCompilance = method.GetCustomAttribute<VariadicTypeAssertion>();
            var typeAssertions = method.GetCustomAttributes<TypeAssertionAttribute>();

            if (primitiveInformation.Variadic && typeAssertions.Any(assertion => assertion.Optional))
            {
                throw new System.InvalidOperationException("Cannot have a variadic function with an optional argument.");
            }

            var assignList = Assign(ListParameter, InputCasted);
            var assignArgumentCount = Assign(ArgumentCount, CallFunction("Count", ListParameter, Constant(false)));
            var methodCallParameters = GenerateMethodCallParameters(typeAssertions, primitiveInformation);
            var methodCall = Call(null, method, methodCallParameters);
            var countCheck = GenerateParameterCountCheck(primitiveInformation, typeAssertions, ArgumentCount);
            var throws = assertions.Any() ? Block(assertions.Select(x =>
            {
                var baseThrow = ThrowIF(x.Expression, x.ErrorMessage);
                return x is IndexAssertionAttribute indexAssertion && indexAssertion.Optional
                    ? IfThen(GreaterThan(ArgumentCount, Constant(indexAssertion.Index)), baseThrow)
                    : baseThrow;
            })) as Expression : Empty();

            var body = Block(new[] { ListParameter, ArgumentCount }, assignList, assignArgumentCount, countCheck, throws, methodCall);

#if DebugMethods
            if (method.Name == "Apply")
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

        static ExpressionList GenerateMethodCallParameters(IEnumerable<TypeAssertionAttribute> typeAssertions,
                                                                    PrimitiveAttribute primitiveInformation)
        {
            if (typeAssertions.Count() > primitiveInformation.InputCount)
                throw new System.InvalidOperationException("Input cannot be less than type assertions");

            var methodCallParameters = GenerateCallParameterCasts(typeAssertions, primitiveInformation.Parameters(ListParameter));
            if (primitiveInformation.Variadic)
            {
                Expression restOfParameters = methodCallParameters.Any()
                                            ? CallFunction("Skip", ListParameter, Constant(primitiveInformation.InputCount))
                                            : ListParameter;

                methodCallParameters = methodCallParameters.Concat(new[] { restOfParameters });
            }

            return methodCallParameters;
        }

    }
}
