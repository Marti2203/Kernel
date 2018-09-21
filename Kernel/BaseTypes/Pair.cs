using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kernel.Utilities;
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

        public Pair(Object car, bool mutable = true)
            : this(car, Null.Instance, mutable)
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
            : this(objects.First(), true)
        {
            if (objects.Count() == 1)
            {
                Cdr = Null.Instance;
            }
            else
            {
                foreach (Object obj in objects.Skip(1))
                    Append(obj);
            }
        }

        public override string ToString()
        {
            if (IsCyclic)
                throw new NotImplementedException("Not yet");
            if (Cdr == null) return Car.ToString();
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
        public override List EvaluateAll(Environment environment) => this.Select<Object>(environment.Evaluate);

        public Pair Append(Object input)
        {
            Pair current = this;
            while (!(current.Cdr is Null) && current.Cdr is Pair p)
                current = p;
            current.Cdr = new Pair(input, Null.Instance);
            return current.Cdr as Pair;
        }

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
            Pair current = this;
            HashSet<Pair> visitedPairsThis = new HashSet<Pair>();
            HashSet<Pair> visitedPairsOther = new HashSet<Pair>();
            while ((visitedPairsThis.Add(current) || visitedPairsOther.Add(p)) && current != null && p != null)
            {
                if (!Car.Equals(p.Car))
                    return false;
                current = current.Cdr as Pair;
                p = p.Cdr as Pair;
            }
            return true;
        }


        public override Object this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "Cannot have negative index");
                Pair current = this;
                while (index-- != 0 && current != null)
                {
                    current = current.Cdr as Pair;
                }
                if (current == null)
                    throw new ArgumentOutOfRangeException(nameof(index), "List is too short for this index.");
                return current.Car;
            }
        }
    }
}
