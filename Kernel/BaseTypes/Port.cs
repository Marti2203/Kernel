using System;
using System.IO;
namespace Kernel.BaseTypes
{
    public sealed class Port : Object, IDisposable
    {
        public static readonly Port StandardInput = new Port(Console.OpenStandardInput(), PortType.Input);
        public static readonly Port StandardOutput = new Port(Console.OpenStandardOutput(), PortType.Output);
        public static readonly Port StandardError = new Port(Console.OpenStandardError(), PortType.Output);

        public static Port CurrentInput = StandardInput;
        public static Port CurrentOutput = StandardOutput;
        public static Port CurrentError = StandardError;

        public PortType Type { get; }
        public TextReader Reader { get; }
        public TextWriter Writer { get; }
        private string FileName { get; }
        public bool IsClosed { get; private set; } = false;

        public Port(Stream s, PortType type)
        {
            Type = type;
            switch (type)
            {
                case PortType.Input:
                    Reader = new StreamReader(s);
                    break;
                case PortType.Output:
                    Writer = new StreamWriter(s);
                    break;
            }
        }
        public Port(string fileName, PortType type)
        {
            Type = type;
            FileName = fileName;
            switch (type)
            {
                case PortType.Input:
                    Reader = new StreamReader(fileName);
                    break;
                case PortType.Output:
                    Writer = new StreamWriter(fileName);
                    break;
            }
        }

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public void Dispose()
        {
            IsClosed = true;
            (Type == PortType.Input ? Reader as IDisposable : Writer).Dispose();
        }

        public override string ToString() => $"{Type} port { FileName ?? StandardOrUnknown }";

        private string StandardOrUnknown => ReferenceEquals(this, StandardError) ? "Standard Error" :
                                            ReferenceEquals(this, StandardOutput) ? "Standard Output" :
                                            ReferenceEquals(this, StandardInput) ? "Standard Input" : "Unknown";
    }
    public enum PortType
    {
        Input, Output
    }
}
