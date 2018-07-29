using System;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
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
				Object input = Get("read").Invoke();
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
				WriteLine($"Output: {result} {result.GetType()} { (result is IEnumerable<Object> list ? list.Count() : -1) }");
			}
		}
	}
}
