using System.Linq;
using Kernel.Utilities;
using Kernel.BaseTypes;

namespace Kernel.Primitives
{
    public static class PredicateApplicative<T> where T : Object
    {
        public static Combiners.Applicative Instance => new Combiners.Applicative(Validate, Name);
        static string Name => typeof(T).Name.ToLower() + "?";
        static Boolean Validate(Object @object)
        {
            if (!(@object is List list))
                throw new System.ArgumentException($"{Name} only accepts a list of objects", nameof(@object));
            return list.All<T>(x => x is T);
        }
    }
}
