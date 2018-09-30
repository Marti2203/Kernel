namespace Kernel
{
    public class String : Object
    {
        public string Data { get; private set; }
        public String(string data)
        {
            this.Data = data;
        }

        public static implicit operator String(string input) => new String(input);
        public static implicit operator string(String @string) => @string.Data;

        public override bool Equals(Object other) => other is String @string && Data == @string.Data;

        public override string ToString() => $"\"{Data}\"";
    }
}
