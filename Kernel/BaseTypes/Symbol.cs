using System;
using System.Collections.Generic;
namespace Kernel.BaseTypes
{
    public sealed class Symbol : Object
    {
        static readonly Dictionary<string, WeakReference<Symbol>> cache = new Dictionary<string, WeakReference<Symbol>>();

        public string Data { get; }

        public static Symbol Get(string data)
        {
            data = data.ToLower();
            if (cache.ContainsKey(data) && cache[data].TryGetTarget(out Symbol value))
                return value;
            Symbol instance = new Symbol(data);
            cache.Add(data, new WeakReference<Symbol>(instance));
            return instance;
        }
        Symbol(string data) => Data = data;
        public override string ToString() => Data;
        public override bool Equals(Object other) => ReferenceEquals(this, other);
        public static implicit operator string(Symbol symb) => symb.Data;
        public static implicit operator Symbol(string data) => Get(data);
    }
}
