using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;
using static System.Linq.Dynamic.DynamicExpression;
namespace Kernel.Primitives
{
	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
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
		public PrimitiveAttribute (string primitiveName, int inputCount = 0, bool variadic = false)
		{
			this.primitiveName = primitiveName;
			this.inputCount = inputCount;
			this.variadic = variadic;
		}

		public string Invocation => string.Join (", ", Enumerable
												 .Range (0, InputCount)
												 .Select (x => $"a[{x}]"));
		// Experimental
		public Expression [] Parameters => Enumerable
			.Range (0, InputCount)
			.Select<int, Expression> (x => ArrayAccess (AssertionAttribute.Array, Constant (x)))
			.ToArray ();
	}

	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	abstract class AssertionAttribute : Attribute
	{
		public readonly string errorMessage;

		protected AssertionAttribute (string errorMessage)
		{
			this.errorMessage = errorMessage;
		}

		public static readonly string ArrayName = "a";
		public abstract Expression Expression { get; }
		public static readonly ParameterExpression Array = Parameter (typeof (Object []), ArrayName);
	}


	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	abstract class IndexAssertionAttribute : AssertionAttribute
	{
		public readonly int index;
		protected IndexAssertionAttribute (int index, string condition, string errorMessage)
			: base (errorMessage)
		{
			this.index = index;
		}

		protected readonly Expression expression;

		protected IndexAssertionAttribute (Expression condition, string errorMessage, int index)
			: base (errorMessage)
		{
			this.index = index;
			expression = condition;
		}

		public override Expression Expression => Not (expression);

		public static Expression ElementAt (int index) => ArrayAccess (Array, Constant (index));
	}

	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class TypeAssertionAttribute : IndexAssertionAttribute
	{
		public readonly Type type;

		public TypeAssertionAttribute (int index, Type type)
			: base (TypeIs (ElementAt (index), type), $"{index} Argument is not a {type}", index)
		{
			this.type = type;
		}
		public TypeAssertionAttribute (int index, Type type1, Type type2)
			: base (OrElse (
				TypeIs (ElementAt (index), type1),
				TypeIs (ElementAt (index), type2)), $"{index} Argument is not a {type1} or {type2}", index)
		{
			this.type = typeof (Object);
		}


	}

	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class PredicateAssertionAttribute : IndexAssertionAttribute
	{
		public PredicateAssertionAttribute (int index, string predicate)
			: base (Call (null, typeof (Primitives).GetMethod (predicate), ElementAt (index)), $"{index} Argument is not valid by {predicate}", index)
		{
		}
	}

	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class MutabilityAssertionAttribute : IndexAssertionAttribute
	{
		public MutabilityAssertionAttribute (int index)
			: base (Property (ElementAt (index), "Mutable"), $"{index} Argument is not mutable", index)
		{

		}
	}


	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	class NonNegativityAssertionAttribute : IndexAssertionAttribute
	{
		public NonNegativityAssertionAttribute (int index)
			: base (And
					(
					 TypeIs (ElementAt (index), typeof (Arithmetic.Integer)),
					 GreaterThan (TypeAs (ElementAt (index), typeof (int)), Constant (0))
					)
				  , $"{index} Argument is a negative integer", index)
		{
		}
	}

	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	class TypeCompilanceAssertionAttribute : AssertionAttribute
	{
		public readonly Type type;

		public TypeCompilanceAssertionAttribute (Type type)
			: base ($"All arguments must be {type.Name}")
		{
			this.type = type;
		}

		public LambdaExpression TypePredicate ()
		{
			ParameterExpression x = Parameter (typeof (Object), "x");
			return Lambda (TypeIs (x, type), x);
		}

		public override Expression Expression => Call (null,
													   typeof (Enumerable).GetMethod ("All").MakeGenericMethod (typeof (Object)),
													   Array,
													   TypePredicate ());

	}


}
