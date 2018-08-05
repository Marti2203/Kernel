using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Kernel
{
	public sealed class Pair : List
	{
		Object car, cdr;
		public Object Car
		{
			get => car;
			set
			{
				if (!Mutable && value is Pair && value.Mutable)
					throw new InvalidOperationException("Mutable pair cannot be part of immutable pair");
				car = value;
			}
		}
		public Object Cdr
		{
			get => cdr;
			set
			{
				if (!Mutable && value is Pair && value.Mutable)
					throw new InvalidOperationException("Mutable pair cannot be part of immutable pair");
				cdr = value;
			}
		}

		public Pair() : this(null, Null.Instance)
		{

		}

		public Pair(Object car, Object cdr, bool mutable = true)
		{
			this.car = car;
			this.cdr = cdr;
			Mutable = mutable;
		}

		public Pair(params Object[] objects) : this(objects as IEnumerable<Object>)
		{

		}

		public Pair(IEnumerable<Object> objects)
		{
			Mutable = true;
			foreach (Object obj in objects)
				Append(obj);
		}

		public override string ToString()
		{
			if (IsCyclic)
				throw new NotImplementedException("Not yet");
			if (Cdr is Null) return $"({Car.ToString()})";
			if (Cdr is Pair p) return ToStringList();
			return $"({Car} . {Cdr})";
		}

		string ToStringList()
		{
			if (IsCyclic)
				throw new NotImplementedException("Not yet");
			StringBuilder result = new StringBuilder(200);

			result.Append('(');

			Pair temp = this;
			do
			{
				result.Append(temp.Car.ToString());
				result.Append(' ');
				if (temp.Cdr is Pair p)
					temp = p;
				else
				{
					if (temp.Cdr != Null.Instance)
					{
						result.Append('.');
						result.Append(' ');
						result.Append(temp.Cdr.ToString());
					}
					else result.Length--;
					break;
				}
			}
			while (temp != null);

			result.Append(')');
			return result.ToString();
		}


		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Kernel.Pair"/> is cyclic.
		/// </summary>
		/// <value><c>true</c> if is cyclic; otherwise, <c>false</c>.</value>
		public override bool IsCyclic => Contains(this);


		public bool Contains(Object o)
		=> Car == o
		|| Cdr == o
		|| (Car is Pair pCar && pCar.Contains(o))
		|| (Cdr is Pair pCdr && pCdr.Contains(o));

		//Todo Will Fuck up with circular lists
		public override Object EvaluateAll(Environment environment)
		{
			if (IsCyclic)
				throw new NotImplementedException();
			return new Pair(this.Select(environment.Evaluate));
		}

		public void Append(Object input)
		{
			if (Car == null)
			{
				Car = input;
				return;
			}
			Pair current = this;
			while (!(current.Cdr is Null) && current.Cdr is Pair p)
				current = p;
			current.Cdr = new Pair(input, Null.Instance);
		}

		public override IEnumerator<Object> GetEnumerator() => new PairEnumerator(this);

#warning This needs to be checked
		public override bool Equals(Object other)
		{
			if (!(other is Pair p)) return false;

			if (IsCyclic ^ p.IsCyclic) return false;
			if (IsCyclic && p.IsCyclic) return CyclicEquality(p);
			return (Car.Equals(p.Car) && Cdr.Equals(p.Cdr));
			//			public bool Equals(Pair other)
			//=> (Car == other.Car && Cdr == other.Cdr)
			//|| (ToString() == other.ToString())
			//|| Equals(other as Object);
		}

		bool CyclicEquality(Pair p)
		{
			throw new NotImplementedException("Not yet");
		}

		class PairEnumerator : IEnumerator<Object>
		{
			Pair CurrentPair { get; set; }
			Pair Start;
			bool firstMove;

			public Object Current => CurrentPair.Car;

			object IEnumerator.Current => CurrentPair.Car;

			public void Dispose()
			{
				CurrentPair = null;
				Start = null;
			}

			// TODO Possible Rework
			public bool MoveNext()
			{
				if (firstMove)
				{
					CurrentPair = CurrentPair.Cdr as Pair;
				}
				else firstMove = true;

				return CurrentPair != null;
			}

			public void Reset()
			{
				CurrentPair = Start;
			}

			public PairEnumerator(Pair start)
			{
				CurrentPair = Start = start;
			}
		}

		public override Object this[int index] => this.ElementAt(index);
	}
}
