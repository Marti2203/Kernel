using System;
using System.Collections.Generic;
namespace Kernel
{
    public sealed class Symbol : Object
    {

        static Dictionary<string, Symbol> cache = new Dictionary<string, Symbol>();

        readonly string data;

        public static Symbol Get(string data)
        {
            data = data.ToLower();
            if (!cache.ContainsKey(data))
                cache.Add(data, new Symbol(data));
            return cache[data];
        }


        Symbol(string data)
        {
            this.data = data;
        }

        public override Object Evaluate(params Object[] input) => Environment.Current[data];

        public override string ToString() => data;
        public override bool Evaluated => false;
    }
}
