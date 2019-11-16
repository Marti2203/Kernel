using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Primitives.BindingAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class OptionalPredicateAssertionAttribute : Attribute
    {
        public OptionalPredicateAssertionAttribute(int index,Type t,string methodName, bool negated = true)
        {

        }
    }
}
