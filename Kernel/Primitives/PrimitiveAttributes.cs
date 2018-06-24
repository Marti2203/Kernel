using System;
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
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class AssertionAttribute : Attribute
    {
        protected readonly string errorMessage;

        protected AssertionAttribute(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
        public abstract string Condition { get; }
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    abstract class ConditionAttribute : AssertionAttribute
    {
        protected readonly string condition;
        protected readonly int index;
        protected string ArgName() => ArgName(index);

        public static string ArgName(int index) => $"a{index}";

        protected ConditionAttribute(int index, string condition, string errorMessage)
            : base(errorMessage)
        {
            this.index = index;
            this.condition = condition;
        }

        public override string Condition
        {
            get => $"!({ArgName()} {condition})";
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    class TypeAssertionAttribute : ConditionAttribute
    {
        public TypeAssertionAttribute(int index, Type type)
            : base(index, $"is {type}", $"{index} Argument is not a {type}")
        {

        }
        public TypeAssertionAttribute(int index, Type type1, Type type2) :
        base(0, $"is {type1} || !({ArgName(index)} is {type2}) ", $"{index} Argument is not a {type1} or {type2}")
        {

        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    class PredicateAssertionAttribute : ConditionAttribute
    {
        public PredicateAssertionAttribute(int index, string predicate)
            : base(index, predicate, $"{index} Argument is not valid by {predicate}")
        {
        }
        public override string Condition => $"!{condition}({ArgName()})";
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    class MutabilityAssertionAttribute : ConditionAttribute
    {
        public MutabilityAssertionAttribute(int index)
            : base(index, $".Mutable", $"{index} Argument is not mutable")
        {

        }
    }


    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    class NonNegativityAssertionAttribute : ConditionAttribute
    {
        public NonNegativityAssertionAttribute(int index)
            : base(index, $"is Integer {ArgName(index)} && {ArgName(index)} > 0 ", $"{index} Argument is a negative integer")
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    class TypeCompilanceAssertionAttribute : AssertionAttribute
    {
        protected readonly string condition;
        protected readonly Type type;

        public TypeCompilanceAssertionAttribute(Type type)
            : base($"All arguments must be {type.Name}")
        {
            this.type = type;
        }

        public override string Condition
        {
            get => $"objects.All(x=> is {type.Name})";
        }
    }


}
