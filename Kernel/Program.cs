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
                }
                catch (ArgumentException argE)
                {
                    WriteLine(argE.Message);
                }
                catch (NoBindingException binding)
                {
                    WriteLine(binding.Message);
                }
                WriteLine($"Output: {result} { result.GetType()} ");
            }
        }
    }
}
