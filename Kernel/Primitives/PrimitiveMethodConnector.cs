using System;
using System.Linq;
using Kernel.Combiners;
using System.Reflection;
namespace Kernel.Primitives{
public static partial class Primitives
{
        static void AddApplicatives()
        {
            foreach (MethodInfo method in typeof(Applicatives)
                    .GetMethods()
                     .Where(method => method.ReturnType.IsSubclassOf(typeof(Object))
                           || method.ReturnType == typeof(Object)))
            {
                PrimitiveAttribute function = method.GetCustomAttribute<PrimitiveAttribute>();
                //IEnumerable<AssertionAttribute> assertions = method.GetCustomAttributes<AssertionAttribute>();
                functions.Add(function.PrimitiveName,
                              new Applicative((Func<Object[], Object>)method
                                              .CreateDelegate(typeof(Func<Object[], Object>))
                                         , function.InputCount
                                         , function.Variadic));
            }
        }

        static void AddOperatives()
        {
            foreach (MethodInfo method in typeof(Operatives)
                    .GetMethods()
                     .Where(method => method.ReturnType.IsSubclassOf(typeof(Object))
                            || method.ReturnType == typeof(Object)))
            {
                PrimitiveAttribute function = method.GetCustomAttribute<PrimitiveAttribute>();
                functions.Add(function.PrimitiveName,
                              new Operative((Func<Environment, Object[], Object>)method
                                            .CreateDelegate(typeof(Func<Environment, Object[], Object>))
                                         , function.InputCount
                                         , function.Variadic));
            }
        }
}
}