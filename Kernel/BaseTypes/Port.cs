using System;
namespace Kernel
{
    public sealed class Port : Object
    {
        public PortType Type { get; }
        public Port(string fileName, PortType type)
        {
            Type = type;
        }

        public override bool Equals(Object other) => ReferenceEquals(this, other);
    }
    public enum PortType
    {
        Input, Output
    }
}
