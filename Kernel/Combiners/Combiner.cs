using System.Diagnostics;
namespace Kernel.Combiners
{
    [DebuggerDisplay("{Name}")]
    public abstract class Combiner : Object
    {
        public string Name { get; }

        public abstract Object Invoke(List list);

        public Object Invoke() => Invoke(Null.Instance);
        public Object Invoke(Object @object) => Invoke(new Pair(@object));
        public Object Invoke(params Object[] objects) => Invoke(new Pair(objects));
        internal Combiner(string name = "Undefined")
        {
            Name = name;
        }

    }
}
