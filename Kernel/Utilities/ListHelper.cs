using System;
using System.Collections.Generic;
namespace Kernel.Utilities
{
    public static class ListHelper
    {
        public static bool All<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list is Null) return true;
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (!predicate(current.Car as T))
                    return false;
                current = current.Cdr as Pair;
            }
            return true;
        }

        public static bool Any<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list is Null) return false;
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (predicate(current.Car as T))
                    return true;
                current = current.Cdr as Pair;
            }
            return false;
        }

        public static Object FirstOrNull<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list is Null) return Null.Instance;
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (predicate(current.Car as T))
                    return current.Car;
                current = current.Cdr as Pair;
            }
            return Null.Instance;
        }

        public static Object First<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list is Null) throw new ArgumentException("Null given.");
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (predicate(current.Car as T))
                    return current.Car;
                current = current.Cdr as Pair;
            }
            throw new ArgumentException("List does not contain an element that passes the predicate");
        }

#pragma warning disable RECS0096 // Type parameter is never used
        public static bool Any<T>(this List list) => !(list is Null);
#pragma warning restore RECS0096 // Type parameter is never used

        public static int Count(this List list)
        {
            if (list.IsCyclic) throw new ArgumentException("Cannot get count of cyclic lists");
            if (list is Null) return 0;
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            int count = 0;
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                count++;
                current = current.Cdr as Pair;
            }
            return count;
        }

        public static int Count<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list.IsCyclic) throw new ArgumentException("Cannot get count of cyclic lists");
            if (list is Null) return 0;
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            int count = 0;
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                count += predicate(current.Car as T) ? 1 : 0;
                current = current.Cdr as Pair;
            }
            return count;
        }


        public static Object Last(this List list)
        {
            if (list is Null) throw new ArgumentException("Cannot find a last element in a null array");
            if (list.IsCyclic) throw new ArgumentException("Cyclic list");
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                current = current.Cdr as Pair;
            }
            return current.Car;
        }

        public static List Select<T>(this List list, Func<T, Object> transform) where T : Object
        {
            if (list is Null) return Null.Instance;
            IDictionary<Pair, Pair> transformations = new Dictionary<Pair, Pair>();
            Pair current = list as Pair;
            Pair resultStart, resultCurrent;
            resultStart = resultCurrent = new Pair(transform(current.Car as T));
            while (current != null && !transformations.ContainsKey(current))
            {
                transformations.Add(current, resultCurrent);
                current = current.Cdr as Pair;
                if (current != null && !transformations.ContainsKey(current))
                    resultCurrent = resultCurrent.Append(transform(current.Car as T));
            }
            if (current != null)
                resultCurrent.Cdr = transformations[current];
            return resultStart;
        }

        public static List Skip(this List list, int count)
        {
            if (list is Null) throw new ArgumentOutOfRangeException(nameof(list), "Cannot skip on an empty list");
            Pair current = list as Pair;
            while (count-- != 0 && current != null)
            {
                current = current.Cdr as Pair;
            }
            if (current == null)
                throw new ArgumentOutOfRangeException(nameof(list), "List is too short for skip");
            return current;
        }

        public static void ForEach<T>(this List list, Action<T> action) where T : Object
        {
            if (list is Null) return;
            Pair current = list as Pair;
            while (current != null)
            {
                action(current.Car as T);
                current = current.Cdr as Pair;
            }
        }

        public static T ForEachReturnLast<T>(this List list, Action<T> action, int skip = 0) where T : Object
        {
            if (list is Null) throw new ArgumentException("Cannot traverse an empty list");
            Pair current = list.Skip(skip) as Pair;
            while (current.Cdr is Pair next)
            {
                action(current.Car as T);
                current = next;
            }
            return current.Car as T;
        }

        //public static T Aggregate<T>(this List list, Func<T, T, T> aggregate) where T : Object
        //{
        //    if (list is Null) throw new InvalidOperationException("What do i doooo?!?!");
        //    Pair pair = list as Pair;
        //    HashSet<Pair> visitedPairs = new HashSet<Pair>();
        //    T result = pair.Car as T;
        //    pair = pair.Cdr as Pair;

        //    while ()
        //}


        public static T[] ToArray<T>(this List list) where T : Object
        {
            if (list is Null) return Array.Empty<T>();
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            List<T> elements = new List<T>();
            while (current != null && visitedPairs.Add(current))
            {
                elements.Add(current.Car as T);
                current = current.Cdr as Pair;
            }
            return elements.ToArray();
        }

        public static List Filter<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            if (list is Null) return Null.Instance;
            Pair current = list as Pair;
            HashSet<Pair> visitedPairs = new HashSet<Pair>();
            while (current != null && !predicate(current.Car as T) && visitedPairs.Add(current))
                current = current.Cdr as Pair;
            if (current == null || visitedPairs.Contains(current))
                return Null.Instance;
            Pair resultStart, resultCurrent;
            resultStart = resultCurrent = new Pair(current.Car);
            Dictionary<Pair, Pair> successfulPairs = new Dictionary<Pair, Pair>{
                { current,resultStart},
                };
            current = current.Cdr as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (predicate(current.Car as T))
                {
                    resultCurrent = resultCurrent.Append(current.Car);
                    successfulPairs.Add(current, resultCurrent);
                }
                current = current.Cdr as Pair;
            }
            if (current != null)
            {
                Pair startOfCycle = current;
                while (!successfulPairs.ContainsKey(current))
                {
                    current = current.Cdr as Pair;
                    if (current == startOfCycle)
                        break;
                }
                resultCurrent.Cdr = successfulPairs[current];
            }
            return resultStart;
        }
    }
}