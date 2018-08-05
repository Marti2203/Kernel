namespace Kernel
{
	public class String : Object
	{
		readonly string data;
		public String(string data)
		{
			this.data = data;
		}

		public override bool Equals(Object other) => other is String @string && data == @string.data;

		public override string ToString() => $"\"{data}\"";
	}
}
