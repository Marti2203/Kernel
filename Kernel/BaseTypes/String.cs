using System;
using System.Collections.Generic;
namespace Kernel
{
    public class String : Object
    {
        static readonly Dictionary<string, WeakReference<String>> cache = new Dictionary<string, WeakReference<String>>();
        public string Data { get; private set; }
        String(string data)
        {
            Data = data;
        }

        public static String Get(string data)
        {
            if (cache.ContainsKey(data) && cache[data].TryGetTarget(out String value))
                return value;
            String instance = new String(data);
            cache.Add(data, new WeakReference<String>(instance));
            return instance;
        }

        public static implicit operator String(string input) => Get(input);
        public static implicit operator string(String @string) => @string.Data;

        public override bool Equals(Object other) => ReferenceEquals(this, other);

        public override string ToString() => $"\"{Data}\"";
    }
}
