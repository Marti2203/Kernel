using System;
using System.IO;
namespace Kernel
{
    public sealed class Port : Object, IDisposable
    {
        public PortType Type { get; }
        public string Path { get; }
        public Port(string fileName, PortType type)
        {
            Path = fileName;
            Type = type;
        }

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public void Dispose()
        {
        }
    }
    public enum PortType
    {
        Input, Output
    }
}
