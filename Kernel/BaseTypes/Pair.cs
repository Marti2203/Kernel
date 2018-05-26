using System;
using System.Collections;
using System.Collections.Generic;
using Kernel.Combiners;

namespace Kernel
{
    public sealed class Pair : Object
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

        public Pair() : this(Null.Instance, Null.Instance)
        {

        }

        public Pair(Object car, Object cdr, bool mutable = true)
        {
            Car = car;
            Cdr = cdr;
            Mutable = mutable;
        }

        public override Object Evaluate(params Object[] input)
        {
            base.Evaluate(input);
            Object f = Car.Evaluate();

            if (f is Operative)
                return f.Evaluate(Cdr.Evaluate(), Environment.Current);
            if (f is Applicative ap)
            {
                if (Cdr is List l)
                    return new TailContext(ap.combiner, l.EvaluateAll());
                throw new ArgumentException("Wrong type of Cdr");
            }
            throw new ArgumentException("Wrong type of Car");
        }

        public override string ToString()
        {
            if (Cdr is Null) return $"({Car})";
            if (Cdr is Pair p && p.Cdr is Null) return $"({Car} {p.Car})";
            return $"({Car}.{Cdr})";
        }

        public bool Equals(Pair other)
        {
            if ((Car == other.Car && Cdr == other.Cdr) || ToString() == other.ToString())
                return true;
            return Equals(other as Object);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Kernel.Pair"/> is cyclic.
        /// </summary>
        /// <value><c>true</c> if is cyclic; otherwise, <c>false</c>.</value>
        public bool IsCyclic => Contains(this);

        public bool Contains(Object o)
        => Car == o
        || Cdr == o
        || (Car is Pair pCar && pCar.Contains(o))
        || (Cdr is Pair pCdr && pCdr.Contains(o));
    }
}
