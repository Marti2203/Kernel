namespace Kernel
{
	public sealed class Boolean : Object
	{
		public static readonly Boolean True = new Boolean(true);
		public static readonly Boolean False = new Boolean(false);
		public bool value;

		Boolean(bool value)
		{
			this.value = value;
		}

		public override string ToString() => this == True ? "#t" : "#f";

		public override bool Equals(Object other) => other == True || other == False;

		public static implicit operator Boolean(bool data) => data ? True : False;
		public static implicit operator bool(Boolean data) => data.value;
	}
}
