//#define DirectRead
using System.Linq;
using Kernel.BaseTypes;
using static System.Console;
using static Kernel.Primitives.Primitives;

namespace Kernel
{
    class Program
    {
        public static void Main()
        {
            Object result;
            int counter = 0;
            while (true)
            {
                result = Null.Instance;
                Write("Kernel> ");
                try
                {
#if DirectRead
                Object input = Applicatives.Read();
#else
                    Object input = Get("read").Invoke();
#endif
                    result = Environment.Ground.Evaluate(input);
                    if (!(result is Inert))
                    {
                        WriteLine($"${++counter} = {result} {result.GetType()}");
                        Environment.Ground[$"${counter}"] = result;
                    }
                }
                catch (System.ArgumentException argE)
                {
                    WriteLine(argE.Message);
                }
                catch (NoBindingException binding)
                {
                    WriteLine(binding.Message);
                }
                catch (System.AggregateException agg)
                {
                    WriteLine(string.Join(System.Environment.NewLine, agg.InnerExceptions.Select(x => x.Message)));
                }
            }
        }
    }
}
