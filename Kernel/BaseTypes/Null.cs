using System;
namespace Kernel
{
    public sealed class Null : Object
    {
        public static readonly Null Instance = new Null();

        Null()
        {
        }

        public override string ToString() => "()";
    }
}
