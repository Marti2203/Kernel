using Kernel.BaseTypes;
using System;
namespace Kernel.Combiners
{
    public sealed class Applicative : Combiner
    {
        public Applicative(Func<List, Object> application, string name = "Undefined")
            : base(name)
        {
            Combiner = new Operative((@object, env) => application(@object), name);
        }

        public Applicative(Combiner combiner)
        {
            Combiner = combiner;
        }

        public Combiner Combiner { get; }

        public override Object Invoke(List list)
        {
            Applicative current = this;
            while (current.Combiner is Applicative next)
                current = next;
            return (current.Combiner as Operative).Invoke(list, Environment.Current);
        }
        public bool Equals(Applicative other) => Combiner == other.Combiner;

        public override string ToString() => Name ?? Combiner.Name;

        public override bool Equals(Object other)
        => ReferenceEquals(this, other) || (other is Applicative app && Combiner == app.Combiner);

    }
}
