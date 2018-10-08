using System;
using System.Collections.Generic;
using Kernel.Combiners;
using static Kernel.Primitives.Primitives;
using System.Linq;
namespace Kernel
{
    public sealed class Environment : Object
    {
        public static readonly Environment Ground = new Environment();
        public static Environment Current = Ground;
        public readonly IEnumerable<Environment> Parents = Enumerable.Empty<Environment>();
        readonly IDictionary<Symbol, Object> bindings = new Dictionary<Symbol, Object>();

        public bool IsStandard => Parents.SequenceEqual(new[] { Ground });

        Environment() { }

        public Environment(Environment parent)
        {
            Parents = new Environment[] { parent };
        }

        public Environment(IEnumerable<Environment> parents)
        {
            Parents = parents.Select(env => env.Copy() as Environment);
        }

        public Object this[Symbol name]
        {
            get => Find(name) ?? throw new NoBindingException($"No binding for {name}");
            set
            {
                if (Has(name))
                    throw new InvalidOperationException("Cannot replace primitive");
                bindings[name] = value;
            }
        }

        public Object this[string name]
        {
            get => this[Symbol.Get(name)];
            set => bindings[Symbol.Get(name)] = value;
        }

        public Object Find(Symbol symbol)
        {
            //if (Has(name)) no return for now
            //    return Get(name);
            HashSet<Environment> traversed = new HashSet<Environment>();
            Stack<Environment> environments = new Stack<Environment>();
            environments.Push(this);

            while (environments.Count != 0)
            {
                Environment current = environments.Pop();
                if (current.bindings.ContainsKey(symbol))
                    return current.bindings[symbol];
                if (traversed.Add(current))
                    foreach (Environment environment in current.Parents)
                        environments.Push(environment);
            }
            if (Has(symbol))
                return Get(symbol);
            return null;
        }

        public bool Contains(Symbol symbol) => Find(symbol) != null;


        public Object Evaluate(Object obj)
        {
            if (obj is Symbol s)
                return this[s];
            if (obj is Pair p)
            {
                Object car = Evaluate(p.Car);
                if (car is Operative o)
                {
                    return o.Invoke(p.Cdr as Pair, this);
                }
                if (car is Applicative ap)
                {
                    if (p.Cdr is List l)
                        return Evaluate(ap.combiner, l.EvaluateAll(this));

                    throw new ArgumentException("Applicatives require a list");
                }
                throw new ArgumentException("Car must be either an applicative or operative");
            }
            return obj;
        }

        public Object Evaluate(Combiner combiner, List list)
        {
            if (combiner is Operative o)
                return o.Invoke(list, this);

            if (combiner is Applicative ap)
                return Evaluate(ap.combiner, list);

            throw new InvalidOperationException("There is another combiner and I do not know of it?!");
        }

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public override string ToString() => "Environment";
    }
}
