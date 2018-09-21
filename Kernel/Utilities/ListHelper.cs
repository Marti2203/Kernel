using System;
using System.Collections.Generic;
namespace Kernel.Utilities
{
    public static class ListHelper
    {
        public static bool All<T>(this List list, Func<T, bool> predicate) where T : Object
        {
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            if (list is Null) return true;
            Pair start, current;
            start = current = list as Pair;
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
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            if (list is Null) return false;
            Pair current = list as Pair;
            while (current != null && visitedPairs.Add(current))
            {
                if (predicate(current.Car as T))
                    return true;
                current = current.Cdr as Pair;
            }
            return false;
        }

        public static bool Any<T>(this List list) => list is Null;

        public static int Count(this List list)
        {
            if (list.IsCyclic) throw new ArgumentException("List cannot be cyclic. The Count is infinity");
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            if (list is Null) return 0;
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
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            if (list is Null) return 0;
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
            ISet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            while (visitedPairs.Add(current))
            {
                if (current.Cdr == Null.Instance)
                    return current.Car;
                current = current.Cdr as Pair;
            }
            throw new ArgumentException("Cyclic list");
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
                if (current != null)
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

        public static T[] ToArray<T>(this List list) where T : Object
        {
            if (list is Null) return Array.Empty<T>();
            HashSet<Pair> visitedPairs = new HashSet<Pair>();
            Pair current = list as Pair;
            List<T> elements = new List<T>();
            while (current != null && visitedPairs.Add(current))
            {
                elements.Add(current.Car as T);
                current = current.Cdr as Pair;
            }
            return elements.ToArray();
        }
    }
}