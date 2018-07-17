using System;
using System.Collections.Generic;
using Kernel.Combiners;
using static Kernel.Primitives.Primitives;
namespace Kernel
{
	public sealed class Environment : Object
	{
		public static readonly Environment Ground = new Environment ();
		public static Environment Current = Ground;
		public readonly IEnumerable<Environment> Parents;
		readonly IDictionary<string, Object> bindings = new Dictionary<string, Object> ();

		public bool IsStandard;

		Environment () { }

		public Environment (Environment parent)
		{
			Parents = new Environment [] { parent };
		}

		public Environment (IEnumerable<Environment> parents)
		{
			Parents = parents;
		}

		public Object this [string name] {
			get {
				if (Has (name))
					return Get (name);
				HashSet<Environment> traversed = new HashSet<Environment> ();
				Stack<Environment> environments = new Stack<Environment> ();
				environments.Push (this);

				while (environments.Count != 0) {
					Environment current = environments.Pop ();
					if (current.bindings.ContainsKey (name))
						return bindings [name];
					if (current.Parents == null) continue;
					if (traversed.Add (current))
						foreach (Environment environment in current.Parents)
							environments.Push (environment);
				}
				if (!Has (name))
					throw new NoBindingException ("No Binding for " + name);
				return Get (name);
			}
		}
		public Object this [Symbol name] {
			get {
				if (Has (name.ToString ()))
					return Get (name.ToString ());
				HashSet<Environment> traversed = new HashSet<Environment> ();
				Stack<Environment> environments = new Stack<Environment> ();
				environments.Push (this);

				while (environments.Count != 0) {
					Environment current = environments.Pop ();
					if (current.bindings.ContainsKey (name.ToString ()))
						return bindings [name.ToString ()];
					if (current.Parents == null) continue;
					if (traversed.Add (current))
						foreach (Environment environment in current.Parents)
							environments.Push (environment);
				}
				throw new NoBindingException ("No Binding for " + name);
			}
			set => bindings [name.ToString ()] = value;
		}

		public Object Evaluate (Object obj)
		{
			if (obj is Symbol s)
				return this [s];
			if (obj is Pair p) {
				object car = Evaluate (p.Car);
				if (car is Operative o) {
					return o.Invoke (p.Cdr as Pair, this);
				}
				if (car is Applicative ap) {
					if (p.Cdr is Pair l) {
						return Evaluate (new Pair (ap.combiner, l.EvaluateAll (this)));
					}
					//throw new NotImplementedException("Not yet");
					throw new ArgumentException ("Applicatives require a list");
				}
				throw new ArgumentException ("Car must be either an applicative or operative");
			}
			return obj;
		}
	}
}
