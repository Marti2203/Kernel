using System;
using System.Collections.Generic;
namespace Kernel
{
    public sealed class Environment : Object
    {
        public static readonly Environment Ground = new Environment();
        public static Environment Current = Ground;
        public readonly IEnumerable<Environment> Parents;
        readonly IDictionary<string, Object> bindings = new Dictionary<string, Object>();

        public bool IsStandard;

        Environment()
        {

        }

        public Environment(Environment parent)
        {
            Parents = new Environment[] { parent };
        }

        public Environment(IEnumerable<Environment> parents)
        {
            Parents = parents;
        }
        public Object this[string name]
        {
            get
            {
                HashSet<Environment> traversed = new HashSet<Environment>();
                Stack<Environment> environments = new Stack<Environment>();
                environments.Push(this);

                while (environments.Count != 0)
                {
                    Environment current = environments.Pop();
                    if (current.bindings.ContainsKey(name))
                        return bindings[name];
                    if (current.Parents == null) continue;
                    if (traversed.Add(current))
                        foreach (Environment environment in current.Parents)
                            environments.Push(environment);
                }
                if(!Primitives.Has(name))
                throw new NoBindingException("No Binding for " + name);
                return Primitives.Get(name);
            }
        }
        public Object this[Symbol name]
        {
            get
            {
                HashSet<Environment> traversed = new HashSet<Environment>();
                Stack<Environment> environments = new Stack<Environment>();
                environments.Push(this);

                while (environments.Count != 0)
                {
                    Environment current = environments.Pop();
                    if (current.bindings.ContainsKey(name.ToString()))
                        return bindings[name.ToString()];
                    if (current.Parents == null) continue;
                    if (traversed.Add(current))
                        foreach (Environment environment in current.Parents)
                            environments.Push(environment);
                }
                throw new NoBindingException("No Binding for " + name);
            }
            set
            {
                bindings[name.ToString()] = value;
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
