namespace Kernel
{
    public abstract class List : Object
    {
        public abstract List EvaluateAll(Environment environment);

        public abstract Object this[int index] { get; }
        public abstract bool ContainsCycle { get; }
    }
}
