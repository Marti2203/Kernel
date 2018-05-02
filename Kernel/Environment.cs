using System;
using System.Collections.Generic;
namespace Kernel
{
    public class Environment
    {
        public static readonly Environment empty = new Environment();
        public Environment parent;
        readonly IDictionary<string, Object> bindings = new Dictionary<string, Object>();

        Environment()
        {

        }

        public Environment(Environment parent)
        {
            this.parent = parent;
        }
        public Object this[string name]
        => bindings.ContainsKey(name) ? bindings[name]
                            : parent == null ? throw new NoBindingException("No Binding for " + name)
                      : parent[name];
    }
}
