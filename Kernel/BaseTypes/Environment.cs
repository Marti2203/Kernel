#define FastEvaluate
using System;
using System.Collections.Generic;
using Kernel.Combiners;
using static Kernel.Primitives.Primitives;
namespace Kernel
{
	public sealed class Environment : Object
	{
		public static readonly Environment Ground = new Environment();
		public static Environment Current = Ground;
		public readonly IEnumerable<Environment> Parents;
		readonly IDictionary<string, Object> bindings = new Dictionary<string, Object>();

		public bool IsStandard;

		Environment() { }

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
				if (Has(name))
					return Get(name);
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
				if (!Has(name))
					throw new NoBindingException("No Binding for " + name);
				return Get(name);
			}
			set => bindings[name] = value;
		}
		public Object this[Symbol name]
		{
			get => this[name.ToString()];
			set => bindings[name.ToString()] = value;
		}

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
					{
#if FastEvaluate
						return Evaluate(ap.combiner, l.EvaluateAll(this));
#else
						return Evaluate(new Pair(ap.combiner, l.EvaluateAll(this)));
#endif
					}
					throw new ArgumentException("Applicatives require a list");
				}
				throw new ArgumentException("Car must be either an applicative or operative");
			}
			return obj;
		}

#if FastEvaluate
        Object Evaluate(Combiner combiner, Object list)
		{
			if (combiner is Operative o)
				return o.Invoke(list, this);

			if (combiner is Applicative ap)
				return Evaluate(ap.combiner, list);

			throw new InvalidOperationException("There is another combiner and I do not know of it?!");
		}
#endif

	}
}
