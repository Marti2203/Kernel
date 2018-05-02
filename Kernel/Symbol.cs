using System;
namespace Kernel
{
    public class Symbol : Object
    {
        string data;

        public Symbol(string data)
        {
            this.data = data;
        }

        public override void Evaluate()
        {
            
        }
    }
}
