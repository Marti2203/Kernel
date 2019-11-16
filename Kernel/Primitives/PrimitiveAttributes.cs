using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Kernel.BaseTypes;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using Object = Kernel.BaseTypes.Object;
namespace Kernel.Primitives
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class PrimitiveAttribute : Attribute
    {
        public string PrimitiveName { get; }
        public int InputCount { get; }
        public bool Variadic { get; }

        public PrimitiveAttribute(string primitiveName, int inputCount = 0, bool variadic = false)
        {
            PrimitiveName = primitiveName;
            InputCount = inputCount;
            Variadic = variadic;
        }

        public Expression[] Parameters(ParameterExpression input) => Enumerable
            .Range(0, InputCount)
            .Select<int, Expression>(x => Property(input, "Item", Constant(x)))
            .ToArray();
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class AssertionAttribute : Attribute
    {
        protected AssertionAttribute(string errorMessage) => ErrorMessage = errorMessage;

        public abstract Expression Expression { get; }

        public static readonly ParameterExpression Input = Parameter(typeof(Object), "@object");

        public static readonly UnaryExpression InputCasted = TypeAs(Input, typeof(List));

        public string ErrorMessage { get; }
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class IndexAssertionAttribute : AssertionAttribute
    {
        protected readonly Expression expression;

        protected IndexAssertionAttribute(Expression condition, string errorMessage, int index, bool negated)
            : base(errorMessage)
        {
            Negated = negated;
            Index = index;
            expression = condition;
        }

        public override Expression Expression => Negated ? Not(expression) : expression;

        public static Expression ElementAt(int index) => Property(InputCasted, "Item", Constant(index));

        public Expression Element => ElementAt(Index);

        public int Index { get; }
        public bool Negated { get; }
    }

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
            : base(OrElse(ElementAtIs((index), type1), ElementAtIs((index), type2)),
                   $"{index} Argument is not a {type1} or {type2}", index, true) => Type = typeof(Object);

        static TypeBinaryExpression ElementAtIs(int index, Type type) => TypeIs(ElementAt(index), type);
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class PredicateAssertionAttribute : IndexAssertionAttribute
    {
        public PredicateAssertionAttribute(int index, Type type, string methodName, bool negated = true)
            : base(StaticCallOnElementAt(type.GetMethod(methodName, new[] { typeof(Object) }), index)
                   , $"{index} Argument is not valid by {methodName}", index, negated)
        { }


        static Expression StaticCallOnElementAt(MethodInfo predicate, int index)
        => Call(null, predicate, ElementAt(index));
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class MutabilityAssertionAttribute : IndexAssertionAttribute
    {
        public MutabilityAssertionAttribute(int index, bool required = true)
            : base(Property(ElementAt(index), "Mutable"), $"{index} Argument is not mutable", index, !required) { }
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class NonNegativityAssertionAttribute : IndexAssertionAttribute
    {
        public NonNegativityAssertionAttribute(int index)
            : base(And
                    (
                     ElementIsInteger(index),
                       GreaterThanOrEqual(TypeAs(ElementAt(index), typeof(Arithmetic.Integer)),
                                          Constant(Arithmetic.Integer.Zero))
                    )
                  , $"{index} Argument is a negative integer", index, true)
        {
        }
        static TypeBinaryExpression ElementIsInteger(int index) => TypeIs(ElementAt(index), typeof(Arithmetic.Integer));
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class VariadicTypeAssertion : AssertionAttribute
    {
        public VariadicTypeAssertion(Type type, int skip = 0)
            : base($"All { (skip > 0 ? $"after {skip}" : "")} arguments must be {type.Name}")
        {
            Skip = skip;
            Type = type;
        }

        public LambdaExpression TypePredicate()
        {
            ParameterExpression x = Parameter(typeof(Object), "x");
            return Lambda(TypeIs(x, Type), x);
        }

        public override Expression Expression
        {
            get
            {
                var input = Skip == 0 ? InputCasted : CallFunction("Skip", InputCasted, Constant(Skip));
                return Not(CallFunction("All", input, TypePredicate()));
            }
        }
        public Type Type { get; }
        public int Skip { get; }
    }

}
