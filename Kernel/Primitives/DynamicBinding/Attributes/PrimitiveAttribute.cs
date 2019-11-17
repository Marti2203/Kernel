using System;
namespace Kernel.Primitives.DynamicBinding.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PrimitiveAttribute : Attribute
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
    }
}
