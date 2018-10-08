using System;

namespace Kernel
{
    public class Continuation : Object
    {
        public static readonly Continuation Base = new Continuation();

        public Continuation Parent { get; private set; }
        public Func<Object, Object> Evaluation { get; private set; }
        Continuation()
        {

        }
        public Continuation(Continuation parent, Func<Object, Object> evaluation)
        {
            Parent = parent;
            Evaluation = evaluation;
        }
        public Object Invoke(Object value) => Evaluation(value);
        public override string ToString() => "Continuation";
        public override bool Equals(Object other) => ReferenceEquals(this, other);
    }
}
