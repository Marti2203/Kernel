namespace Kernel.BaseTypes
{
    public class Ignore : Object
    {
        public static readonly Ignore Instance = new Ignore();

        Ignore() {}

        public override bool Equals(Object other) => other is Ignore;

        public override string ToString() => "#ignore";
    }
}

