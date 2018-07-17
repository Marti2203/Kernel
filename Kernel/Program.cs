using System;
using static System.Console;
using static Kernel.Primitives.Primitives;
namespace Kernel
{
    class Program
    {
        public static void Main ()
        {
            while (true) {
                // Hack no need for cast if you know what you want
                Write ("Input: ");
                Object input = Get ("read").Invoke ();
                //WriteLine($"{input.ToString()} {input.GetType()}");
                Object result = Environment.Ground.Evaluate (input);
                WriteLine ($"Output: {result}");
            }
        }
    }
}
