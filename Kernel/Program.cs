//#define DirectRead
using System;
using static System.Console;
using static Kernel.Primitives.Primitives;

namespace Kernel
{
    class Program
    {
        public static void Main()
        {
            int counter = 1;
            while (true)
            {
                Write("Input: ");
#if DirectRead
                Object input = Applicatives.Read();
#else
                Object input = Get("read").Invoke();
#endif

                //WriteLine($"{input.ToString()} {input.GetType()}");
                Object result = Null.Instance;
                try
                {
                    result = Environment.Ground.Evaluate(input);
                    if (!(result is Inert))
                    {
                        WriteLine($"Output: ${counter} = {result} { result.GetType()} ");
                        Environment.Ground[$"${counter++}"] = result;
                    }
                }
                catch (ArgumentException argE)
                {
                    WriteLine(argE.Message);
                }
                catch (NoBindingException binding)
                {
                    WriteLine(binding.Message);
                }
            }
        }
    }
}
