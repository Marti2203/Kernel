#define FastCopyEs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kernel.Combiners;
using Kernel.Arithmetic;
using System.Text;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using Kernel.Utilities;
using static System.Linq.Expressions.Expression;

namespace Kernel.Primitives
{
	public static class Primitives
	{
		public static Combiner Get (string name)
		=> functions.ContainsKey (name) ? functions [name] : throw new NoBindingException ("No such primitive");
		public static bool Has (string name)
		=> functions.ContainsKey (name);

		static readonly IDictionary<string, Combiner> functions;

		static Primitives ()
		{
			functions = new Dictionary<string, Combiner> ();
			//AddPredicateApplicatives ();
			AddApplicatives ();
			//AddOperatives ();
			//AddCarFamily ();
		}

		static void AddApplicatives ()
		{
			foreach (MethodInfo method in typeof (Applicatives)
					 .GetMethods ()
					 .Where (method => method.ReturnType.IsOrIsSubclassOf (typeof (Object)))) {

				PrimitiveAttribute primitiveInformation = method
					.GetCustomAttribute<PrimitiveAttribute> ();
				IEnumerable<AssertionAttribute> assertions = method
					.GetCustomAttributes<AssertionAttribute> ();

				Expression [] parameters = new Expression [0];

				if (assertions.Any (x => x is TypeCompilanceAssertionAttribute)) {
					TypeCompilanceAssertionAttribute assertion = assertions.First (x => x is TypeCompilanceAssertionAttribute)
																 as TypeCompilanceAssertionAttribute;

					MethodInfo Cast = typeof (Enumerable).GetMethod ("Cast").MakeGenericMethod (assertion.type);
					MethodInfo ToArray = typeof (Enumerable).GetMethod ("ToArray").MakeGenericMethod (assertion.type);
					parameters = new [] { Call (null, ToArray, Call (null, Cast, AssertionAttribute.Array)) };
				} else {
					IEnumerable<TypeAssertionAttribute> typeAssertions = method.GetCustomAttributes<TypeAssertionAttribute> ();
					parameters = primitiveInformation.InputCount == 0 ? new Expression [0] : primitiveInformation
													 .Parameters
													 .Select ((expression, index) => {
														 TypeAssertionAttribute assertion = typeAssertions.FirstOrDefault (x => x.index == index);
														 return assertion == null ? expression : TypeAs (expression, assertion.type);
													 }).ToArray ();
				}


				Expression Throw (AssertionAttribute assertion)
				{

					Console.WriteLine (primitiveInformation.PrimitiveName);
					Console.WriteLine ($"The assertion is {assertion}");
					return IfThen (assertion.Expression,
							 Expression.Throw (Constant (new ArgumentException
														(assertion.errorMessage))));
				}
				var function = Lambda (Block (assertions
											  .Select (Throw)
											  .Concat (new Expression [] { Call (null, method, parameters) }))
						, true, AssertionAttribute.Array);
				Applicative app = new Applicative (function.Compile () as Func<Object [], Object>
										 , primitiveInformation.InputCount
										 , primitiveInformation.Variadic);
				functions.Add (primitiveInformation.PrimitiveName, app);
			}
		}

		static void AddOperatives ()
		{
			foreach (MethodInfo method in typeof (Operatives)
					.GetMethods ()
					 .Where (method => method.ReturnType.IsSubclassOf (typeof (Object))
							 || method.ReturnType == typeof (Object))) {
				PrimitiveAttribute function = method.GetCustomAttribute<PrimitiveAttribute> ();
				Operative op = new Operative ((Func<Environment, Object [], Object>)method
											 .CreateDelegate (typeof (Func<Environment, Object [], Object>))
										 , function.InputCount
										 , function.Variadic);
				functions.Add (function.PrimitiveName, op);
			}
		}

		public static bool IsTailContext (Object obj)
		=> ((obj is Pair p) && (p.Car is Combiner) /* p.Cdr is Pair? */);

		static void AddPredicateApplicatives ()
		{
			foreach (Type t in typeof (Object)
					 .Assembly
					 .GetTypes ()
					 .Where (t => t.IsSubclassOf (typeof (Object))))
				functions.Add (t.Name.ToLower () + "?", typeof (PredicateApplicative<>)
						 .MakeGenericType (t)
						 .GetProperty ("Instance")
						 .GetValue (null) as Combiner);
		}

		static void AddCarFamily ()
		{
			foreach (MethodInfo method in typeof (CarFamily)
					 .GetMethods ()
					 .Where (method => method.ReturnType == typeof (Object)))
				functions.Add (method.Name.ToLower (),
						 new Applicative ((Func<Object [], Object>)method.CreateDelegate (typeof (Func<Object [], Object>))
										 , 1));
		}

		public static bool IsFormalParameterTree (Object @object) => IsFormalParameterTree (@object, true);

		public static void Match (Environment env, Object definiend, Object result)
		{
			switch (definiend) {
			case Symbol s:
				env [s] = result;
				break;
			case Null n:
				if (!(result is Null))
					throw new ArgumentException ("Expression must be null");
				break;
			case Pair pDefiniend:
				if (!(result is Pair pExpression))
					throw new ArgumentException ("Expression must be a pair");
				Match (env, pDefiniend.Car, pExpression.Car);
				Match (env, pDefiniend.Cdr, pExpression.Cdr);
				break;
			}
		}


		static bool IsFormalParameterTree (Object @object, bool pairCheck)
		=> @object is Symbol
		|| @object is Ignore
		|| @object is Null
		|| (pairCheck && @object is Pair p && IsFormalParameterTree (p));

		static bool IsFormalParameterTree (Pair p)
		{
			HashSet<Symbol> containedSymbols = new HashSet<Symbol> ();
			Stack<Pair> pairs = new Stack<Pair> ();
			pairs.Push (p);
			do {
				p = pairs.Pop ();
				if (p.IsCyclic) throw new ArgumentException ("Cannot accept a cyclic tree");
				if (p.Car is Pair pCar)
					pairs.Push (pCar);
				else {
					if (p.Car is Symbol sCar && !containedSymbols.Add (sCar))
						throw new ArgumentException ($"Parameter tree contains a duplicate of symbol {sCar}");
					if (!IsFormalParameterTree (p.Car, false))
						return false;
				}
				if (p.Cdr is Pair pCdr)
					pairs.Push (pCdr);
				else {
					if (p.Cdr is Symbol sCdr && !containedSymbols.Add (sCdr))
						throw new ArgumentException ($"Parameter tree contains a duplicate of symbol {sCdr}");
					if (!IsFormalParameterTree (p.Cdr, false))
						return false;
				}
			}
			while (pairs.Count != 0);
			return true;
		}

		static class Applicatives
		{
			[Primitive ("read")]
			public static Object Read ()
			{
				bool Valid (string representation)
				{
					int braces = 0;
					bool inString = false;
					for (int i = 0; i < representation.Length; i++) {
						switch (representation [i]) {
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
					return braces == 0 && !inString && representation.Trim ().Length != 0;
				}

				string input = Console.ReadLine ();

				while (!Valid (input))
					input += Console.ReadLine ();

				return Parser.Parse (input.Trim ());
			}

			[Primitive ("set-car", 2)]
			[TypeAssertion (0, typeof (Pair))]
			[MutabilityAssertion (0)]
			public static Inert SetCar (Pair p, Object obj)
			{
				p.Car = obj;
				return Inert.Instance;
			}

			[Primitive ("set-cdr", 2)]
			[TypeAssertion (0, typeof (Pair))]
			[MutabilityAssertion (0)]
			public static Inert SetCdr (Pair p, Object obj)
			{
				p.Cdr = obj;
				return Inert.Instance;
			}

			[Primitive ("copy-es-immutable", 1)]
			// Todo Cyclic pairs?
			public static Object CopyEvaluationStructureImmutable (Object obj)
			{
				if (!(obj is Pair p)) return obj;

				// HACK Why bother with creating a new one?
#if FastCopyES
                if (!p.Mutable) return p; 
#endif
				return new Pair (CopyEvaluationStructureImmutable (p.Car),
								CopyEvaluationStructureImmutable (p.Cdr), false);
			}

			[Primitive ("eval", 2)]
			[TypeAssertion (1, typeof (Environment))]
			[PredicateAssertion (0, nameof (IsTailContext))]
			public static Object Eval (Object tailContext, Environment env) => env.Evaluate (tailContext);

			[Primitive ("make-environment", 0, true)]
			[TypeCompilanceAssertion (typeof (Environment))]
			public static Environment MakeEnvironment (params Environment [] environments)
			=> new Environment (environments);

			[Primitive ("equal?", 0, true)]
			public static Boolean Equal (params Object [] objects)
			{
				if (objects.Length == 0) return Boolean.True;
				if (objects.All (obj => obj.GetType () == objects [0].GetType () && obj.Equals (objects [0]))) return Boolean.True;
				return Boolean.False;
			}

			[Primitive ("wrap", 1)]
			[TypeAssertion (0, typeof (Combiner))]
			public static Combiner Wrap (Combiner c) => new Applicative (c);

			[Primitive ("unwrap", 1)]
			[TypeAssertion (0, typeof (Applicative))]
			public static Combiner Unwrap (Applicative app) => app.combiner;


			[Primitive ("list", 1, true)]
			public static Object List (params Object [] objects)
			{
				if (objects.Length == 1) return objects [0];
				Pair head = new Pair (objects [0], Null.Instance);
				Pair tail = head;
				for (int i = 1; i < objects.Length; i++) {
					tail.Cdr = new Pair (objects [i], Null.Instance);
					tail = tail.Cdr as Pair;
				}
				return head;
			}

			[Primitive ("list*", 1, true)]
			public static Object ListStar (params Object [] objects)
			{
				if (objects.Length == 1) return objects [0];
				Pair head = new Pair (objects [0], Null.Instance);
				Pair tail = head;
				for (int i = 1; i < objects.Length - 1; i++) {
					tail.Cdr = new Pair (objects [i], Null.Instance);
					tail = tail.Cdr as Pair;
				}
				tail.Cdr = objects [objects.Length - 1];
				return head;
			}

			[Primitive ("apply", 2, true)]
			[TypeAssertion (0, typeof (Applicative))]
			[TypeAssertion (1, typeof (Pair))]
			[TypeAssertion (2, typeof (Environment))]
#warning This function may get an environment, what do i do??
			public static Object Apply (Applicative a, Pair p, Environment e) => e.Evaluate (new Pair (a.combiner, p));

			public static class ListMetrics
			{
				public static Integer Pairs (Pair metric)
				=> CarFamily.Car (metric) as Integer;
				public static Boolean WithNull (Pair metric)
				=> (Boolean)((CarFamily.Cdar (metric) as Integer) != 0);
				public static Boolean Cyclic (Pair metric)
				=> (Boolean)((CarFamily.Cdddar (metric) as Integer) != 0);
			}

			static readonly Dictionary<Object, Pair> immutableMetricsCache = new Dictionary<Object, Pair> ();
			[Primitive ("get-list-metrics", 1)]
			public static Pair GetListMetrics (Object obj)
			{
				if (obj.Mutable && immutableMetricsCache.ContainsKey (obj))
					return immutableMetricsCache [obj];
				int pairs = 0;
				int nilCount = 0;
				int acyclicLength = 0;
				int cycleLength = 0;
				if (obj is Pair p) {
					Dictionary<Pair, int> visits = new Dictionary<Pair, int> ();
					while (p != null) {
						if (visits [p] == 2)
							break;
						visits.Add (p, 1);
						if (p.Cdr is Pair pNew)
							p = pNew;
						else if (p.Cdr is Null) { nilCount++; break; }
					}
					acyclicLength = visits.Count (v => v.Value == 1);
					cycleLength = visits.Count (v => v.Value == 2);
					pairs = acyclicLength + cycleLength;
				}
				Pair result = List (
					new Integer (pairs),
					new Integer (nilCount),
					new Integer (acyclicLength),
					new Integer (cycleLength)
				) as Pair;
				if (obj.Mutable)
					immutableMetricsCache.Add (obj, result);
				return result;
			}

			[Primitive ("list-tail", 2)]
			[TypeAssertion (1, typeof (Integer))]
			[NonNegativityAssertion (1)]
			public static Object ListTail (Object obj, Integer i)
			{
				Object result = obj;
				for (int depth = 0; depth < i; depth++) {
					if (!(result is Pair p))
						throw new ArgumentException ("First argument is not a deep enough list");
					result = p.Cdr;
				}
				return result;
			}

			[Primitive ("encycle!", 3)]
			[TypeAssertion (1, typeof (Integer))]
			[NonNegativityAssertion (1)]
			[TypeAssertion (2, typeof (Integer))]
			[NonNegativityAssertion (2)]
			public static Object Encycle (Object obj, Integer i1, Integer i2)
			{
				if (i2 == Integer.Zero)
					return Inert.Instance;

				Object start = ListTail (obj, (i1 + i2 - 1));

				if (!(start is Pair p))
					throw new ArgumentException ("List is not deep enough to encycle");

				SetCdr (p, ListTail (obj, i1));

				return Inert.Instance;
			}


			[Primitive ("map", 1, true)]
			[TypeAssertion (0, typeof (Applicative))]
			public static Object Map (Applicative app, Pair [] lists)
			{
				//TODO WHAT THE FUCK DO WE DO WITH CYCLIC LISTS?!!!?!?!
				if (lists.Length == 0)
					throw new InvalidOperationException ("Lists list must not be empty");
				Integer starterLength = ListMetrics.Pairs (GetListMetrics (lists [0]));
				if (lists.Length > 1 && !lists.All (l => ListMetrics.Pairs (GetListMetrics (l)) == starterLength))
					throw new InvalidOperationException ("A list does not have the same length as the others");

				Pair result = new Pair ();
				while (lists [0] != null) {
					result.Append (app.combiner.Invoke (lists.Select (x => x.Car).ToArray ()));
					lists = lists.Select (x => x.Cdr as Pair).ToArray ();
				}
				return result;
			}

			[Primitive ("eq?", 0, true)]
			public static Boolean Eq (Object [] objects)
			=> (Boolean)(objects.Length == 0 || objects.All (obj => obj.Equals (objects [0])));
		}

		static class Operatives
		{
			[Primitive ("$lambda", 2)]
			[TypeAssertion (0, typeof (Environment))]
#pragma warning disable RECS0154 // Parameter is never used
			public static Combiner Lambda (Environment env, Object formals, Object [] objects)
#pragma warning restore RECS0154 // Parameter is never used
			=> Applicatives.Wrap (Vau (Environment.Current, formals, Ignore.Instance, objects));

			[Primitive ("$if", 3)]
			[PredicateAssertion (1, nameof (IsTailContext))]
			[PredicateAssertion (2, nameof (IsTailContext))]
			public static Object If (Environment env, Object testInput, Object consequent, Object alternative)
			{
#warning TailContext issues
				Object test = env.Evaluate (testInput);
				if (!(test is Boolean condition)) throw new ArgumentException ("Test is not a boolean.");

				return env.Evaluate (condition ? consequent : alternative);
			}

			[Primitive ("$define", 2)]
			[PredicateAssertion (0, nameof (IsFormalParameterTree))]
			public static Inert Define (Environment env, Object definiend, Object expr)
			{
				Match (env, definiend, env.Evaluate (expr));
				return Inert.Instance;
			}

			[Primitive ("$vau", 3, true)]
			[PredicateAssertion (0, nameof (IsFormalParameterTree))]
			[TypeAssertion (2, typeof (Symbol))]
			[TypeAssertion (1, typeof (Symbol), typeof (Ignore))]
			public static Operative Vau (Environment env, Object formals, Object eformal, params Object [] expr)
			{
				if ((formals is Symbol && formals == eformal)
					|| (formals is Pair p && eformal is Symbol && p.Contains (eformal)))
					throw new ArgumentException ("Formals contain eformal");

				return new Operative (Environment.Current.Copy () as Environment,
									 Applicatives.CopyEvaluationStructureImmutable (formals),
									 eformal,
									 expr.Length == 1 ?
									 Applicatives.CopyEvaluationStructureImmutable (expr [0])
									 : Applicatives.CopyEvaluationStructureImmutable (Sequence (env, expr)));

			}

			[Primitive ("$sequence", 0, true)]
			public static Object Sequence (Environment env, params Object [] objects)
			{
				if (objects.Length == 0) return Inert.Instance;
				for (int i = 0; i < objects.Length - 1; i++)
					env.Evaluate (objects [i]);
				if (IsTailContext (objects [objects.Length - 1]))
					return env.Evaluate (objects [objects.Length - 1]);
				throw new InvalidProgramException ("WTF!?");
			}

			[Primitive ("$cond", 0, true)]
			[TypeCompilanceAssertion (typeof (Pair))]
			public static Object Cond (Environment env, params Pair [] objects)
			{
				foreach (Pair p in objects) {
					Object test = env.Evaluate (p.Car);
					if (!(test is Boolean condition))
						throw new ArgumentException ($"Test in {p.Car} is not a boolean.");

					if (condition)
						return Sequence (env, p.Cdr);
				}
				return Inert.Instance;
			}

			[Primitive ("cons", 2)]
#pragma warning disable RECS0154 // Parameter is never used
			public static Object Cons (Environment env, Object car, Object cdr)
#pragma warning restore RECS0154 // Parameter is never used
			=> new Pair { Car = car, Cdr = cdr };
		}
	}
}