#define FastCopyES
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kernel.Combiners;
using Kernel.Arithmetic;
using static Kernel.Primitives.DynamicConnections;
using Kernel.Utilities;
namespace Kernel.Primitives
{
	public static class Primitives
	{
		public static Combiner Get(string name)
		=> functions.ContainsKey(name) ? functions[name] : throw new NoBindingException("No such primitive");
		public static bool Has(string name)
		=> functions.ContainsKey(name);

		static readonly IDictionary<string, Combiner> functions;

		static Primitives()
		{
			functions = new Dictionary<string, Combiner>();
			AddPredicateApplicatives();
			AddApplicatives();
			AddCarFamily();
			AddOperatives();
		}


		static void AddApplicatives()
		{
			foreach (MethodInfo method in typeof(Applicatives)
					 .GetMethods()
					 .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
			{
				PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
				var pipedMethod = CreatePipeFunction(method);
				Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
				functions.Add(primitiveInformation.PrimitiveName, app);
			}
		}


		static void AddOperatives()
		{
			foreach (MethodInfo method in typeof(Operatives)
					 .GetMethods()
					 .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
			{
				PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
				Operative app = new Operative(CreatePipeFunction(method), primitiveInformation.PrimitiveName);
				functions.Add(primitiveInformation.PrimitiveName, app);
			}
		}



		static void AddPredicateApplicatives()
		{
			foreach (Type t in typeof(Object)
					 .Assembly
					 .GetTypes()
					 .Where(t => t.IsSubclassOf(typeof(Object)) && !t.IsAbstract))
				functions.Add(t.Name.ToLower() + "?", typeof(PredicateApplicative<>)
							  .MakeGenericType(t)
							  .GetProperty("Instance")
							  .GetValue(null) as Combiner);
		}

		static void AddCarFamily() // Optimised version of Add(Applicatives|Operatives) for the car methods
		{
			foreach (MethodInfo method in typeof(CarFamily)
					 .GetMethods()
					 .Where((MethodInfo method) => method.ReturnType.IsOrIsSubclassOf(typeof(Object))))
			{
				PrimitiveAttribute primitiveInformation = method.GetCustomAttribute<PrimitiveAttribute>();
				var pipedMethod = CreatePipeFunction(method);
				Applicative app = new Applicative(pipedMethod, primitiveInformation.PrimitiveName);
				functions.Add(primitiveInformation.PrimitiveName, app);
			}

		}

#warning This must be reworked
		public static bool IsTailContext(Object obj)
		=> true; //(obj is Pair p && p.Car is Combiner);

		public static Object Evaluate(Object @object, Environment environment)
		=> environment.Evaluate(@object);

		public static bool IsFormalParameterTree(Object @object) => IsFormalParameterTree(@object, true);

		public static void Match(Environment env, Object definiend, Object result)
		{
			switch (definiend)
			{
				case Symbol s:
					env[s] = result;
					break;
				case Null n:
					if (!(result is Null))
						throw new ArgumentException("Expression must be null");
					break;
				case Pair pDefiniend:
					if (!(result is Pair pExpression))
						throw new ArgumentException("Expression must be a pair");
					Match(env, pDefiniend.Car, pExpression.Car);
					Match(env, pDefiniend.Cdr, pExpression.Cdr);
					break;
			}
		}


		static bool IsFormalParameterTree(Object @object, bool pairCheck)
		=> @object is Symbol
		|| @object is Ignore
		|| @object is Null
		|| (pairCheck && @object is Pair p && IsFormalParameterTree(p));

		static bool IsFormalParameterTree(Pair p)
		{
			HashSet<Symbol> containedSymbols = new HashSet<Symbol>();
			Stack<Pair> pairs = new Stack<Pair>();
			pairs.Push(p);
			do
			{
				p = pairs.Pop();
				if (p.IsCyclic) throw new ArgumentException("Cannot accept a cyclic tree");
				if (p.Car is Pair pCar)
					pairs.Push(pCar);
				else
				{
					if (p.Car is Symbol sCar && !containedSymbols.Add(sCar))
						throw new ArgumentException($"Parameter tree contains a duplicate of symbol {sCar}");
					if (!IsFormalParameterTree(p.Car, false))
						return false;
				}
				if (p.Cdr is Pair pCdr)
					pairs.Push(pCdr);
				else
				{
					if (p.Cdr is Symbol sCdr && !containedSymbols.Add(sCdr))
						throw new ArgumentException($"Parameter tree contains a duplicate of symbol {sCdr}");
					if (!IsFormalParameterTree(p.Cdr, false))
						return false;
				}
			}
			while (pairs.Count != 0);
			return true;
		}

		public static class Applicatives
		{
			[Primitive("cons", 2)]
			public static Object Cons(Object car, Object cdr) => new Pair { Car = car, Cdr = cdr };

			[Primitive("display", 1)]
			public static Inert Display(Object input)
			{
				Console.WriteLine(input);
				return Inert.Instance;
			}

			[Primitive("exit")]
			public static Inert Exit()
			{
				System.Environment.Exit(0);
				return Inert.Instance;
			}

			[Primitive("read")]
			public static Object Read()
			{
				bool Valid(string representation)
				{
					int braces = 0;
					bool inString = false;
					for (int i = 0; i < representation.Count(); i++)
					{
						switch (representation[i])
						{
							case '(':
								if (!inString) braces++;
								break;
							case ')':
								if (!inString) braces--;
								break;
							case '"':
								inString = !inString;
								break;
							case '\\':
								i++;
								break;
						}
					}
					return braces == 0 && !inString && representation.Trim().Any();
				}

				string input = Console.ReadLine();

				while (!Valid(input))
					input += Console.ReadLine();

				return Parser.Parse(input.Trim());
			}

			[Primitive("set-car!", 2)]
			[TypeAssertion(0, typeof(Pair))]
			[MutabilityAssertion(0)]
			public static Inert SetCar(Pair p, Object obj)
			{
				p.Car = obj;
				return Inert.Instance;
			}


			[Primitive("set-cdr!", 2)]
			[TypeAssertion(0, typeof(Pair))]
			[MutabilityAssertion(0)]
			public static Inert SetCdr(Pair p, Object obj)
			{
				p.Cdr = obj;
				return Inert.Instance;
			}

			[Primitive("copy-es-immutable", 1)]
			// Todo Cyclic pairs?
			public static Object CopyEvaluationStructureImmutable(Object obj)
			{
				if (!(obj is Pair p)) return obj;

				// HACK Why bother with creating a new one?
#if FastCopyES
				if (!p.Mutable) return p;
#endif
				return new Pair(CopyEvaluationStructureImmutable(p.Car),
								CopyEvaluationStructureImmutable(p.Cdr), false);
			}

			[Primitive("eval", 2)]
			[TypeAssertion(1, typeof(Environment))]
			[PredicateAssertion(0, nameof(IsTailContext))]
			public static Object Eval(Object tailContext, Environment env) => env.Evaluate(tailContext);

			[Primitive("make-environment", 0, true)]
			[TypeCompilanceAssertion(typeof(Environment))]
			public static Environment MakeEnvironment(params Environment[] environments)
			=> new Environment(environments);

			[Primitive("equal?", 0, true)]
			public static Boolean Equal(List objects)
			{
				if (!objects.Any()) return Boolean.True;
				if (objects.All(obj => obj.GetType() == objects[0].GetType() && obj.Equals(objects[0]))) return Boolean.True;
				return Boolean.False;
			}

			[Primitive("wrap", 1)]
			[TypeAssertion(0, typeof(Combiner))]
			public static Combiner Wrap(Combiner c) => new Applicative(c);

			[Primitive("unwrap", 1)]
			[TypeAssertion(0, typeof(Applicative))]
			public static Combiner Unwrap(Applicative app) => app.combiner;


			[Primitive("list", 0, true)]
			public static Object List(List objects)
			{
				if (!objects.Any()) return Null.Instance;
				if (objects.Count() == 1) return objects[0];
				Pair head = new Pair(objects[0]);
				Pair tail = head;
				for (int i = 1; i < objects.Count(); i++)
				{
					tail.Cdr = new Pair(objects[i], Null.Instance);
					tail = tail.Cdr as Pair;
				}
				return head;
			}

			[Primitive("list*", 0, true)]
			public static Object ListStar(List objects)
			{
				if (!objects.Any()) return Null.Instance;
				if (objects.Count() == 1) return objects[0];
				Pair head = new Pair(objects[0], Null.Instance);
				Pair tail = head;
				for (int i = 1; i < objects.Count() - 1; i++)
				{
					tail.Cdr = new Pair(objects[i], Null.Instance);
					tail = tail.Cdr as Pair;
				}
				tail.Cdr = objects[objects.Count() - 1];
				return head;
			}

			[Primitive("apply", 3, false)]
			[TypeAssertion(0, typeof(Applicative))]
			[TypeAssertion(1, typeof(Pair))]
			[TypeAssertion(2, typeof(Environment))]
#warning This function may not get an environment, what do i do??
			public static Object Apply(Applicative a, Pair p, Environment e) => e.Evaluate(new Pair(a.combiner, p));

			public static class ListMetrics
			{
				public static Integer Pairs(Pair metric)
				=> CarFamily.Car(metric) as Integer;
				public static Boolean WithNull(Pair metric)
				=> (Boolean)((CarFamily.Cdar(metric) as Integer) != 0);
				public static Boolean Cyclic(Pair metric)
				=> (Boolean)((CarFamily.Cdddar(metric) as Integer) != 0);
			}

			static readonly Dictionary<Object, Pair> immutableMetricsCache = new Dictionary<Object, Pair>();
			[Primitive("get-list-metrics", 1)]
			public static Pair GetListMetrics(Object obj)
			{
				if (!obj.Mutable && immutableMetricsCache.ContainsKey(obj))
					return immutableMetricsCache[obj];
				int pairs = 0;
				int nilCount = 0;
				int acyclicLength = 0;
				int cycleLength = 0;
				if (obj is Pair p)
				{
					Dictionary<Pair, int> visits = new Dictionary<Pair, int>();
					while (p != null)
					{
						if (visits.TryGetValue(p, out int visitsCount) && visitsCount == 2)
							break;
						visits.Add(p, 1);
						if (p.Cdr is Pair pNew)
							p = pNew;
						else
						{
							if (p.Cdr is Null) nilCount++;
							break;
						}
					}
					acyclicLength = visits.Count(v => v.Value == 1);
					cycleLength = visits.Count(v => v.Value == 2);
					pairs = acyclicLength + cycleLength;
				}
				Pair result = List(new Pair(
					new Integer(pairs),
					new Integer(nilCount),
					new Integer(acyclicLength),
					new Integer(cycleLength)
				)) as Pair;
				if (!obj.Mutable)
					immutableMetricsCache.Add(obj, result);
				return result;
			}

			[Primitive("list-tail", 2)]
			[TypeAssertion(1, typeof(Integer))]
			[NonNegativityAssertion(1)]
			public static Object ListTail(Object obj, Integer i)
			{
				Object result = obj;
				for (int depth = 0; depth < i; depth++)
				{
					if (!(result is Pair p))
						throw new ArgumentException("First argument is not a deep enough list");
					result = p.Cdr;
				}
				return result;
			}

			[Primitive("encycle!", 3)]
			[TypeAssertion(1, typeof(Integer))]
			[NonNegativityAssertion(1)]
			[TypeAssertion(2, typeof(Integer))]
			[NonNegativityAssertion(2)]
			public static Object Encycle(Object obj, Integer i1, Integer i2)
			{
				if (i2 == Integer.Zero)
					return Inert.Instance;

				Object start = ListTail(obj, (i1 + i2 - 1));

				if (!(start is Pair p))
					throw new ArgumentException("List is not deep enough to encycle");

				SetCdr(p, ListTail(obj, i1));

				return Inert.Instance;
			}


			[Primitive("map", 1, true)]
			[TypeAssertion(0, typeof(Applicative))]
			[TypeCompilanceAssertion(typeof(Pair), 1)]
			public static Object Map(Applicative app, Pair[] lists)
			{
				// TODO WHAT THE FUCK DO WE DO WITH CYCLIC LISTS?!!!?!?!
				// Rework
				if (lists.Length == 0)
					throw new InvalidOperationException("Lists list must not be empty");
				Integer starterLength = ListMetrics.Pairs(GetListMetrics(lists[0]));
				if (lists.Count() > 1 && !lists.All(l => ListMetrics.Pairs(GetListMetrics(l)) == starterLength))
					throw new InvalidOperationException("A list does not have the same length as the others");
				if (lists[0].IsCyclic && !lists.All(list => list.IsCyclic))
					throw new InvalidOperationException("A list is cyclic, so they must all be");
				Pair result = new Pair();
				while (lists[0] != null)
				{
					if (app.combiner is Operative o)
						result.Append(o.Invoke(new Pair(lists.Select(pair => pair.Car)), Environment.Current));
					if (app.combiner is Applicative ap)
						result.Append(ap.Invoke(new Pair(lists.Select(pair => pair.Car))));
					lists = lists.Select(x => x.Cdr as Pair).ToArray();
				}
				return result;
			}

			[Primitive("eq?", 0, true)]
			public static Boolean Eq(List objects)
			=> (Boolean)(!objects.Any() || objects.All(obj => obj.Equals(objects[0])));

			[Primitive("get-current-environment", 0)]
			public static Environment GetEnvironment()
			=> Environment.Current;

		}

		public static class Operatives
		{
			[Primitive("$lambda", 3)]
			[TypeAssertion(0, typeof(Environment))]
			[TypeAssertion(2, typeof(Pair))]
			public static Combiner Lambda(Environment env, Object formals, Pair objects)
			=> Applicatives.Wrap(Vau(Environment.Current, formals, Ignore.Instance, objects));

			[Primitive("$if", 4)]
			[TypeAssertion(0, typeof(Environment))]
			[PredicateAssertion(2, nameof(IsTailContext))]
			[PredicateAssertion(3, nameof(IsTailContext))]
			public static Object If(Environment env, Object testInput, Object consequent, Object alternative)
			{
				Object test = env.Evaluate(testInput);
				if (!(test is Boolean condition)) throw new ArgumentException("Test is not a boolean.");

				return env.Evaluate(condition ? consequent : alternative);
			}

			[Primitive("$define", 3)]
			[TypeAssertion(0, typeof(Environment))]
			[PredicateAssertion(1, nameof(IsFormalParameterTree))]
			public static Inert Define(Environment env, Object definiend, Object expr)
			{
				Match(env, definiend, env.Evaluate(expr));
				return Inert.Instance;
			}

			[Primitive("$vau", 4)]
			[TypeAssertion(0, typeof(Environment))]
			[PredicateAssertion(1, nameof(IsFormalParameterTree))]
			[TypeAssertion(2, typeof(Symbol), typeof(Ignore))]
			[TypeAssertion(3, typeof(Pair))]
			public static Operative Vau(Environment env, Object formals, Object eformal, Pair expr)
			{
				if ((formals is Symbol && formals == eformal)
					|| (formals is Pair p && eformal is Symbol && p.Contains(eformal)))
					throw new ArgumentException("Formals contain eformal");

				return new Operative(Environment.Current.Copy() as Environment,
									 Applicatives.CopyEvaluationStructureImmutable(formals),
									 eformal,
									  expr.Count() == 1 ?
									 Applicatives.CopyEvaluationStructureImmutable(expr[0])
									 : Applicatives.CopyEvaluationStructureImmutable(Sequence(env, expr)));

			}

			[Primitive("$sequence", 2)]
			[TypeAssertion(0, typeof(Environment))]
			[TypeAssertion(1, typeof(Pair))]
			public static Object Sequence(Environment env, Pair objects)
			{
				if (!objects.Any()) return Inert.Instance;
				for (int i = 0; i < objects.Count() - 1; i++)
					env.Evaluate(objects[i]);
				if (IsTailContext(objects[objects.Count() - 1]))
					return env.Evaluate(objects[objects.Count() - 1]);
				throw new InvalidProgramException("WTF!?");
			}

			[Primitive("$cond", 1, true)]
			[TypeAssertion(0, typeof(Environment))]
			[TypeCompilanceAssertion(typeof(Pair), 1)]
			public static Object Cond(Environment env, Pair[] objects)
			{
				foreach (Pair p in objects)
				{
					Object test = env.Evaluate(p.Car);
					if (!(test is Boolean condition))
						throw new ArgumentException($"Test in {p.Car} is not a boolean.");

					if (condition)
						return Sequence(env, p.Cdr as Pair);
				}
				return Inert.Instance;
			}

		}
	}
}