using System.Collections;
using System.Collections.Generic;
namespace Kernel
{
    public abstract class List : Object, IEnumerable<Object>
    {
        public abstract Object EvaluateAll(Environment environment);
        public abstract IEnumerator<Object> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public abstract Object this[int index] { get; }
        public abstract bool IsCyclic { get; }
    }
}
