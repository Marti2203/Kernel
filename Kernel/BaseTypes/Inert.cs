using System;

namespace Kernel
{
    public class Inert : Object
    {
        public static readonly Inert Instance = new Inert();

        Inert()
        {
        }

        public override string ToString() => "#inert";
    }
}
