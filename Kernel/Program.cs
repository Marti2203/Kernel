using System;
using static System.Console;
using System.Linq;
namespace Kernel
{
    class Program
    {
        public static void Main()
        {
            while (true)
                WriteLine(Environment.Ground["read"].Evaluate().ToString());
        }
    }
}
