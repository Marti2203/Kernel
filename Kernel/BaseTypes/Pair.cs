using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            this.car = car;
            this.cdr = cdr;
			Mutable = mutable;
        }

        public override string ToString()
        {
            if (IsCyclic)
                throw new NotImplementedException();
            if (Cdr is Null) return $"({Car.ToString()})";
            if (Cdr is Pair p) return ToStringList();
            return $"({Car} . {Cdr})";
        }

        string ToStringList()
        {

            StringBuilder result = new StringBuilder();

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
                    if (temp.Cdr is Null)
                        result.Length--;
                    else
                        result.Append(temp.Cdr.ToString());
                    break;
                }
            }
            while (temp != null);

            result.Append(')');
            return result.ToString();
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

        //Todo Will Fuck up with circular lists
        public Pair EvaluateAll()
        {
            if (IsCyclic)
                throw new NotImplementedException();
            Pair resultHead;
            Pair resultTail;
            resultHead = resultTail = new Pair(Environment.Current.Evaluate(Car),Null.Instance);
            Pair list = Cdr as Pair; 
            while(list!=null)
            {
                resultTail.Cdr = new Pair(Environment.Current.Evaluate(list.Cdr), Null.Instance);
                resultTail = resultTail.Cdr as Pair;
                list = list.Cdr as Pair;
            }
            return resultHead;
        }

        public void Append(Object input)
        {
            if(Car is Null)
            {
                Car = input;
                return;
            }
            Pair current = this;
            while (!(current.Cdr is Null) && current.Cdr is Pair p)
                current = p;
            current.Cdr = new Pair(input, Null.Instance);
        }
    }
}
