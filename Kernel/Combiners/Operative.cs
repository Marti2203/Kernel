using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using static Kernel.Primitives.Primitives;
namespace Kernel.Combiners
{
	[DebuggerDisplay("{Name}")]
	public sealed class Operative : Combiner
	{
		readonly IOperative underlyingOperative;

		public Operative(Func<Object, Environment, Object> operation, string name = "Undefined")
			: base(name)
		{
			underlyingOperative = new PrimitiveOperative(operation);
		}

		internal Operative(Func<Object, Object> operation, string name = "Undefined")
			: this((@object, environment) => operation(new Pair(environment, @object)), name)
		{

		}

		public Operative(Environment env, Object formals, Object eformal, Object expr)
			: base("User Defined Operative")
		{
			underlyingOperative = new CompoundOperative(env, formals, eformal, expr);
		}

#warning Have to Implement equality and hash code methods for the underlying operatives
		interface IOperative
		{
			Object Action(Object @object, Environment environment);
		}

		class CompoundOperative : IOperative
		{
			readonly Environment @static;
			readonly Object formals;
			readonly Object eformal;
			readonly Object expr;

			public CompoundOperative(Environment @static, Object formals, Object eformal, Object expr)
			{
				this.@static = @static;
				this.formals = formals;
				this.eformal = eformal;
				this.expr = expr;
			}
			public Object Action(Object @object, Environment dynamicEnvironment)
			{
				Environment local = new Environment(@static);
				Match(local, formals, @object);
				if (eformal is Symbol s)
					local[s] = dynamicEnvironment;
				if (IsTailContext(expr))
					return local.Evaluate(expr);
				throw new ArgumentException("Expression is not a tail context");
			}

		}

		class PrimitiveOperative : IOperative
		{
			readonly Func<Object, Environment, Object> operation;
			public PrimitiveOperative(Func<Object, Environment, Object> operation)
			{
				this.operation = operation;
			}

			public Object Action(Object @object, Environment environment)
			{
				return operation(@object, environment);
			}

		}

		public override Object Invoke(Object @object)
		{
			if (@object is Pair p && p.Cdr is Environment environment)
				return underlyingOperative.Action(p.Car, environment);
			throw new ArgumentException("Argument is not a Pair or Cdr is not an Environment");
		}

		public Object Invoke(Pair pair, Environment environment) => underlyingOperative.Action(pair, environment);

		public bool Equals(Operative other) => underlyingOperative == other.underlyingOperative;

		public override string ToString() => Name;
	}
}
