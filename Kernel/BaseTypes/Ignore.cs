using System;
namespace Kernel
{
    public class Ignore : Object
    {
        public static readonly Ignore Instance = new Ignore();

        Ignore()
        {
        }

        public override string ToString() => "#ignore";
    }
}

