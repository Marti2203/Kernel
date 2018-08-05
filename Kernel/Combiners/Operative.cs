using System;
using System.Diagnostics;
using static Kernel.Primitives.Primitives;
using System.Linq;
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

		// This is Used only by the Add Operatives Method
		internal Operative(Func<Object, Object> operation, string name = "Undefined")
			: this((@object, environment) => operation(new Pair(environment, @object)), name)
		{

		}

		public Operative(Environment env, Object formals, Object eformal, Object expr)
			: base("User Defined Operative")
		{
			underlyingOperative = new CompoundOperative(env, formals, eformal, expr);
		}

		interface IOperative
		{
			Object Action(Object @object, Environment environment);
		}

		#region Underlying Operatives

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
				return Operatives.Sequence(local, (expr as List).ToArray());
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

		#endregion

		public override Object Invoke(Object @object)
		{
			if (@object is Pair p && p.Cdr is Environment environment)
				return underlyingOperative.Action(p.Car, environment);
			throw new ArgumentException("Argument is not a Pair or Cdr is not an Environment");
		}

		public Object Invoke(Object @object, Environment environment) => underlyingOperative.Action(@object, environment);

		public override string ToString() => Name;

		public override bool Equals(Object other)
		=> (ReferenceEquals(this, other))
		|| (other is Operative operative && underlyingOperative == operative.underlyingOperative);
	}
}
