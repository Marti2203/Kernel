using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel
{
    //Todo Rework List class
    public class List : Object
    {
        public static List Empty = new List();
        readonly Pair head;
        Pair tail;

        public Object Last
        {
            set
            {
                tail.Cdr = value;
            }
        }

        public List() : this(Null.Instance) { }

        public List(Object car)
        {
            head = tail = new Pair { Car = car, Cdr = Null.Instance };
        }

        public List(IEnumerable<Object> objects)
        {
            head = tail = new Pair();
            foreach(Object obj in objects)
                Append(obj);
        }

        public void Append(Object @object)
        {
            if (tail.Car is Null)
                tail.Car = @object;
            else
            {
                if (!(tail.Cdr is Null))
                    throw new InvalidOperationException("Cannot append to a dotted list");

                Pair @new = new Pair { Car = @object, Cdr = Null.Instance };
                tail.Cdr = @new;
                tail = @new;
            }
        }

        //Todo Will Fuck up with circular lists
        public List EvaluateAll()
        {
            Pair temp = head;
            List result = new List();
            do
            {
                result.Append(temp.Car.Evaluate());

                temp = temp.Cdr as Pair;

            } while (temp != tail);
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append('(');

            Pair temp = head;
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
            while (temp != head);

            result.Append(')');
            return result.ToString();
        }
    }
}
 