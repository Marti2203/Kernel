using System.Diagnostics;
namespace Kernel.Combiners
{
    [DebuggerDisplay("{Name}")]
    public abstract class Combiner : Object
    {
        public string Name => name;
        readonly string name;

        public abstract Object Invoke(List list);

        public Object Invoke() => Invoke(Null.Instance);
        public Object Invoke(Object @object) => Invoke(new Pair(@object));
        public Object Invoke(params Object[] objects) => Invoke(new Pair(objects));
        protected Combiner(string name = "Undefined")
        {
            this.name = name;
        }

    }
}
