using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static Kernel.Utilities.MethodCallUtilities;
using System.Reflection;

namespace Kernel.Primitives
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class PrimitiveAttribute : Attribute
	{
		/// <summary>
		/// The name of the primitive.
		/// </summary>
		readonly string primitiveName;
		/// <summary>
		/// The input count of the combiner.
		/// </summary>
		readonly int inputCount;
		/// <summary>
		/// Is Variadic.
		/// </summary>
		readonly bool variadic;
		/// <summary>
		/// Gets the name of the primitive.
		/// </summary>
		/// <value>The name of the primitive.</value>
		public string PrimitiveName => primitiveName;
		/// <summary>
		/// Gets the input count.
		/// </summary>
		/// <value>The input count.</value>
		public int InputCount => inputCount;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Kernel.PrimitiveAttribute"/> is variadic.
		/// </summary>
		/// <value><c>true</c> if variadic; otherwise, <c>false</c>.</value>
		public bool Variadic => variadic;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Kernel.PrimitiveAttribute"/> class.
		/// </summary>
		/// <param name="primitiveName">Primitive name.</param>
		/// <param name="inputCount">Input count.</param>
		/// <param name="variadic">If set to <c>true</c> variadic.</param>
		public PrimitiveAttribute(string primitiveName, int inputCount = 0, bool variadic = false)
		{
			this.primitiveName = primitiveName;
			this.inputCount = inputCount;
			this.variadic = variadic;
		}

		public Expression[] Parameters(ParameterExpression input) => Enumerable
			.Range(0, InputCount)
			.Select<int, Expression>(x => Property(input, "Item", Constant(x)))
			.ToArray();
	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	abstract class AssertionAttribute : Attribute
	{
		readonly string errorMessage;

		protected AssertionAttribute(string errorMessage)
		{
			this.errorMessage = errorMessage;
		}

		public abstract Expression Expression { get; }
		public static readonly ParameterExpression Input = Parameter(typeof(Object), "@object");
        public static readonly UnaryExpression InputCasted = TypeAs(Input, typeof(List));


		public string ErrorMessage => errorMessage;
	}


	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	abstract class IndexAssertionAttribute : AssertionAttribute
	{
		readonly int index;

		protected readonly Expression expression;

		protected IndexAssertionAttribute(Expression condition, string errorMessage, int index)
			: base(errorMessage)
		{
			this.index = index;
			expression = condition;
		}

		public override Expression Expression => Not(expression);

		public static Expression ElementAt(int index) => Property(InputCasted, "Item", Constant(index));

		public int Index => index;
	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class TypeAssertionAttribute : IndexAssertionAttribute
	{
		readonly Type type;

		public TypeAssertionAttribute(int index, Type type)
			: base(TypeIs(ElementAt(index), type), $"{index} Argument is not a {type}", index)
		{
			this.type = type;
		}
		public TypeAssertionAttribute(int index, Type type1, Type type2)
			: base(OrElse(
				TypeIs(ElementAt(index), type1),
				TypeIs(ElementAt(index), type2)), $"{index} Argument is not a {type1} or {type2}", index)
		{
			this.type = typeof(Object);
		}
		public Type Type => type;

	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class PredicateAssertionAttribute : IndexAssertionAttribute
	{
		public PredicateAssertionAttribute(int index, string predicate)
			: base(Call(null, typeof(Primitives).GetMethod(predicate), ElementAt(index)), $"{index} Argument is not valid by {predicate}", index)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class MutabilityAssertionAttribute : IndexAssertionAttribute
	{
		public MutabilityAssertionAttribute(int index)
			: base(Property(ElementAt(index), "Mutable"), $"{index} Argument is not mutable", index)
		{

		}
	}


	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class NonNegativityAssertionAttribute : IndexAssertionAttribute
	{
		public NonNegativityAssertionAttribute(int index)
			: base(And
					(
					 TypeIs(ElementAt(index), typeof(Arithmetic.Integer)),
                       GreaterThanOrEqual(PropertyOrField(TypeAs(ElementAt(index),typeof(Arithmetic.Integer)),"data"),
                                          Constant(Arithmetic.Integer.Zero))
					)
				  , $"{index} Argument is a negative integer", index)
		{
		}
	}

	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	class TypeCompilanceAssertionAttribute : AssertionAttribute
	{
		readonly Type type;
		readonly int skip;

		public TypeCompilanceAssertionAttribute(Type type, int skip = 0)
			: base($"All { (skip > 0 ? $"after {skip}" : "")} arguments must be {type.Name}")
		{
			this.skip = skip;
			this.type = type;
		}

		public LambdaExpression TypePredicate()
		{
			ParameterExpression x = Parameter(typeof(Object), "x");
			return Lambda(TypeIs(x, type), x);
		}

		public override Expression Expression
		{
			get
			{
				if (skip == 0)
					return CallEnumerable<Object>("All", InputCasted, TypePredicate());

				return Not(CallEnumerable<Object>("All",
						   CallEnumerable<Object>("ToArray",
						   CallEnumerable<Object>("Skip",
												  InputCasted,
												  Constant(skip))),
												  TypePredicate()));

			}
		}
		public Type Type => type;
		public int Skip => skip;
	}
}
