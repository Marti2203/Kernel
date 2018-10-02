//#define PrintNonStandart
using System;
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
            if (Cdr is Null) return $"({Car.ToString()})";
            if (Cdr is Pair p) return ToStringList(Array.Empty<Pair>());
            return $"({Car} . {Cdr})";
        }

        string ToStringList(Pair[] visitedCars, int depth = 0)
        {
            StringBuilder result = new StringBuilder(200);

            result.Append('(');
            HashSet<Pair> visitedPairs = new HashSet<Pair>();
            Pair mainTree = this;
            while (mainTree != null && visitedPairs.Add(mainTree) && !visitedCars.Contains(mainTree))
            {
                if (mainTree.Car is Pair pair)
                {
                    if (pair == this)
                    {
                        result.Append($"#{1 - visitedPairs.Count}#");
                    }
                    else
                    {
                        if (visitedCars.Contains(pair))
                            result.Append($"#{1 - (depth + visitedPairs.Count + visitedCars.Length)}#");
                        else
                            result.Append(pair.ToStringList(visitedCars.Concat(new[] { this }).ToArray()
                                                            , depth + visitedPairs.Count - 1));
                    }
                }
                else
                    result.Append(mainTree.Car);
                result.Append(' ');
                if (mainTree.Cdr is Pair p)
                    mainTree = p;
                else
                {
                    if (mainTree.Cdr != Null.Instance)
                    {
                        result.Append('.')
                              .Append(' ')
                              .Append(mainTree.Cdr);
                    }
                    else result.Length--;
                    mainTree = null;
                    break;
                }
            }
            if (mainTree != null)
            {
                if (visitedCars.Contains(mainTree))
                {
#warning Extra or deficient one here, be careful
                    result.Append($". #{-(visitedPairs.Count + depth)}#");
                }
                else
                {
                    Pair start = mainTree;
                    int counter = 0;
                    while (mainTree.Cdr != start)
                    {
                        counter++;
                        mainTree = mainTree.Cdr as Pair;
                    }
                    result.Append($". #{-counter}#");
                }
            }

            result.Append(')');
            return result.ToString();
        }


        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Kernel.Pair"/> is cyclic.
        /// </summary>
        /// <value><c>true</c> if is cyclic; otherwise, <c>false</c>.</value>
        public override bool IsCyclic => Contains(this);


        public bool Contains(Object o)
        {
            if (Car == o || Cdr == o) return true;
            HashSet<Pair> visitedPairs = new HashSet<Pair>();
            Stack<Pair> pairs = new Stack<Pair>();
            pairs.Push(this);
            while (pairs.Count != 0)
            {
                Pair current = pairs.Pop();
                if (current.Car == o || current.Cdr == o) return true;
                if (visitedPairs.Add(current))
                {
                    if (current.Car is Pair pCar)
                    {
                        pairs.Push(pCar);
                    }
                    if (current.Cdr is Pair pCdr)
                    {
                        pairs.Push(pCdr);
                    }
                }
            }
            return false;
        }

        public override List EvaluateAll(Environment environment) => this.Select<Object>(environment.Evaluate);

        public Pair Append(Object input)
        {
            Pair current = this, result = new Pair(input, Null.Instance);
            while (!(current.Cdr is Null) && current.Cdr is Pair p)
                current = p;
            current.Cdr = result;
            return result;
        }

        public override bool Equals(Object other)
        {
            if (!(other is Pair p)) return false;
            Pair current = this;
            HashSet<Pair> visitedPairsThis = new HashSet<Pair>();
            HashSet<Pair> visitedPairsOther = new HashSet<Pair>();
            while (current != null && p != null && (visitedPairsThis.Add(current) || visitedPairsOther.Add(p)))
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Object other)) return false;
            if (Mutable != other.Mutable) return false;
            if (!(other is Pair p)) return false;
            return Equals(other);
        }
    }
}
