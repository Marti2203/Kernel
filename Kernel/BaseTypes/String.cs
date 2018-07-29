using System;
namespace Kernel
{
    public class String : Object
    {
        readonly string data;
        public String(string data)
        {
            this.data = data;
        }

        public override string ToString() => $"\"{data}\"";
    }
}
