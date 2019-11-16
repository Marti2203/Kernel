namespace Kernel.BaseTypes
{
    public class Inert : Object
    {
        public static readonly Inert Instance = new Inert();

        Inert(){}

        public override bool Equals(Object other) => other is Inert;

        public override string ToString() => "#inert";
    }
}
