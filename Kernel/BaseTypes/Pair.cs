//#define PrintNonStandart
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kernel.Utilities;

namespace Kernel.BaseTypes
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public sealed class Pair : List
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        Object car, cdr;
        public Object Car
        {
            get => car;
            set
            {
                if (!Mutable)
                    throw new InvalidOperationException("Cannot mutate an immutable object");
                if (!Mutable && value is Pair && value.Mutable)
                    throw new InvalidOperationException("Mutable pair cannot be part of immutable pair");
                if (Contains(value))
                {
                    IsCyclic = true;
                }
                car = value;
            }
        }
        public Object Cdr
        {
            get => cdr;
            set
            {
                if (!Mutable)
                    throw new InvalidOperationException("Cannot mutate an immutable object");
                if (!Mutable && value is Pair && value.Mutable)
                    throw new InvalidOperationException("Mutable pair cannot be part of immutable pair");
                if (Contains(value))
                {
                    IsCyclic = true;
                }
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
        => Cdr is Null ? $"({Car.ToString()})" : Cdr is Pair ? ToStringList(Array.Empty<Pair>()) : $"({Car} . {Cdr})";

        string ToStringList(Pair[] visitedCars, int depth = 0)
        {
            StringBuilder result = new StringBuilder(10);

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
                    //May Diverge by 1 from Guile(I do not know why!)
                    result.Append($". #{ 1 - (visitedPairs.Count + depth)}#");
                }
                else
                {
                    Pair start = mainTree;
                    int counter = 1;
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


        private bool containsCycle = false;
        public override bool IsCyclic { get => containsCycle; protected set => containsCycle = value ; }

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
                if (!IsCyclic || visitedPairs.Add(current))
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

        public Pair Tail
        {
            get
            {
                Pair current = this;
                while (!(current.Cdr is Null) && current.Cdr is Pair p)
                    current = p;
                return current;
            }
        }

        public override bool Equals(Object other)
        {
            if (!(other is Pair otherPair)) return false;
            if (Car != otherPair.Car) return false; // quick escape hatch
            if (IsCyclic != otherPair.IsCyclic) return false;
            Pair current = this;
            
            return IsCyclic ? CyclicalEquality(ref otherPair, ref current) : ACyclicalEquality(ref otherPair, ref current);
        }
        private static bool ACyclicalEquality(ref Pair otherPair, ref Pair current)
        {
            while (current != null && otherPair != null)
            {
                if (!current.Car.Equals(otherPair.Car))
                    return false;
                if (current.Cdr is Pair next && otherPair.Cdr is Pair otherNext)
                {
                    current = next;
                    otherPair = otherNext;
                }
                else
                {
                    return !(current.Cdr is Pair || otherPair.Cdr is Pair) && current.Cdr.Equals(otherPair.Cdr);
                }
            }
            return true;
        }
        private static bool CyclicalEquality(ref Pair otherPair, ref Pair current)
        {
            HashSet<Pair> visitedPairsThis = new HashSet<Pair>();
            HashSet<Pair> visitedPairsOther = new HashSet<Pair>();
            while (current != null && otherPair != null && (visitedPairsThis.Add(current) || visitedPairsOther.Add(otherPair)))
            {
                if (!current.Car.Equals(otherPair.Car))
                    return false;
                if (current.Cdr is Pair next && otherPair.Cdr is Pair otherNext)
                {
                    current = next;
                    otherPair = otherNext;
                }
                else
                {
                    return !(current.Cdr is Pair || otherPair.Cdr is Pair) && current.Cdr.Equals(otherPair.Cdr);
                }
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
        => ReferenceEquals(this, obj)
            || (obj is Object other
            && Mutable == other.Mutable
            && Equals(other));
    }
}
