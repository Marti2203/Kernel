using System;

namespace Kernel.BaseTypes
{
    public abstract class List : Object
    {
        public static List Empty = Null.Instance;
        public abstract List EvaluateAll(Environment environment);

        public abstract Object this[int index] { get; }
        public abstract bool IsCyclic { get; protected set; }

    }
}
