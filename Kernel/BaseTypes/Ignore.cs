using System;
namespace Kernel
{
	public class Ignore : Object
	{
		public static readonly Ignore Instance = new Ignore();

		Ignore()
		{
		}

		public override bool Equals(Object other) => other is Inert;

		public override string ToString() => "#ignore";
	}
}

