using System;
namespace Kernel.Combiners
{
    public sealed class Applicative : Combiner
    {
        public Applicative(Func<List, Object> application, string name = "Undefined")
            : base(name)
        {
            combiner = new Operative((@object, env) => application(@object), name);
        }

        public Applicative(Combiner combiner)
        {
            this.combiner = combiner;
        }

        public Combiner Combiner => combiner;
        readonly Combiner combiner;

        public override Object Invoke(List list)
        {
            Applicative current = this;
            while (current.combiner is Applicative next)
                current = next;
            return (current.combiner as Operative).Invoke(list, Environment.Current);
        }
        public bool Equals(Applicative other) => Combiner == other.Combiner;

        public override string ToString() => Name ?? Combiner.Name;

        public override bool Equals(Object other)
        => ReferenceEquals(this, other) || (other is Applicative app && Combiner == app.Combiner);

    }
}
