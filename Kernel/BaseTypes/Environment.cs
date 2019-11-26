using System;
using System.Collections.Generic;
using Kernel.Combiners;
using static Kernel.Primitives.Primitives;
using System.Linq;

namespace Kernel.BaseTypes
{
    public sealed class Environment : Object
    {
        public static readonly Environment Ground = new Environment();
        public static Environment Current = Ground;
        public readonly IEnumerable<Environment> ProperParents = Enumerable.Empty<Environment>();
        readonly IDictionary<Symbol, Object> bindings = new Dictionary<Symbol, Object>
        {
            {Symbol.Get("+inf.0"), Arithmetic.Real.PositiveInfinity},
            {Symbol.Get("-inf.0"), Arithmetic.Real.NegativeInfinity}
        };

        public bool IsStandard => ProperParents.SequenceEqual(new[] { Ground });

        Environment() { }

        public Environment(Environment parent)
        {
            ProperParents = new Environment[] { parent };
        }

        internal Guid ID { get; }
        internal Object Value { get; }

        internal Environment(Environment parent, Guid id, Object value)
            : this(parent)
        {
            ID = id;
            Value = value;
        }

        public Environment(IEnumerable<Environment> parents)
        {
            ID = Guid.NewGuid();
            ProperParents = parents;
        }

        public Object this[Symbol name]
        {
            get => Find(name) ?? throw new NoBindingException($"No binding for {name}");

            set
            {
                if (Has(name))
                    throw new InvalidOperationException("Cannot replace primitive");
                var container = ProperParents.FirstOrDefault(parent => parent.Contains(name)) ?? this;
                container.bindings[name] = value;
            }
        }

        public Object this[string name]
        {
            get => this[Symbol.Get(name)];
            set => bindings[Symbol.Get(name)] = value;
        }

        public Object Find(Symbol symbol)
        {
            HashSet<Environment> traversed = new HashSet<Environment>();
            Stack<Environment> environmentsToTraverse = new Stack<Environment>();
            environmentsToTraverse.Push(this);

            while (environmentsToTraverse.Count != 0)
            {
                Environment current = environmentsToTraverse.Pop();
                if (current.bindings.ContainsKey(symbol))
                    return current.bindings[symbol];
                if (traversed.Add(current))
                    foreach (Environment environmentParents in current.ProperParents)
                        if (!traversed.Contains(environmentParents) && !environmentsToTraverse.Contains(environmentParents))
                            environmentsToTraverse.Push(environmentParents);
            }
            return Has(symbol) ? Get(symbol) : null;
        }

        public IEnumerable<Environment> ImproperParents
        {
            get
            {
                HashSet<Environment> traversed = new HashSet<Environment>();
                Stack<Environment> environments = new Stack<Environment>();
                environments.Push(this);

                while (environments.Count != 0)
                {
                    Environment current = environments.Pop();
                    if (traversed.Add(current))
                        foreach (Environment environment in current.ProperParents)
                            environments.Push(environment);
                }
                return traversed;
            }
        }

        public bool Contains(Symbol symbol) => Find(symbol) != null;


        public Object Evaluate(Object obj)
        {
            Object EvaluatePair(Pair p)
            {
                Object car = Evaluate(p.Car);
                if (!(p.Cdr is List l))
                {
                    throw new ArgumentException("Applicatives require a list");
                }
                return car switch
                {
                    Operative o => o.Invoke(p.Cdr as Pair, this),
                    Applicative ap => Evaluate(ap.Combiner,l.EvaluateAll(this)),
                    _ => throw new ArgumentException("Car must be either an applicative or operative"),
                };
            }

            return obj switch
            {
                Symbol s => this[s],
                Pair p => EvaluatePair(p),
                _ => obj,
            };
        }

        public Object Evaluate(Combiner combiner, List list)
        {
            if (combiner is Applicative ap)
            {
                while (ap.Combiner is Applicative innerAp)
                    ap = innerAp;
                return Evaluate(ap.Combiner, list);
            }
            return (combiner as Operative).Invoke(list, this);
        }

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public override string ToString() => $"Environment #{ID}";
    }
}
