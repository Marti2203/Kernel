using System;
using System.Collections.Generic;
namespace Kernel
{
    public static class EvaluatedPrimitives
    {
        static readonly IDictionary<string, Object> data = new Dictionary<string, Object>();
        static EvaluatedPrimitives()
        {
            data.Add(Create("CopyEsImmutable.k"));
			data.Add(Create("LambdaCore.k"));

            data.Add("list-core",Primitives.Get("eval")
                     .Evaluate(new String("($define! list (wrap ($vau x #ignore x)))"),Environment.Ground));
            data.Add("list-special",Primitives.Get("eval")
                     .Evaluate(new String("($define! list ($lambda x x))"),Environment.Ground));
            
            data.Add(Create("SequenceCore.k"));
            data.Add(Create("SequenceSpecial.k"));

            data.Add(Create("ListStarCore.k"));
            data.Add(Create("ListStarSpecial.k"));

            data.Add(Create("VauCore.k"));
            data.Add(Create("VauSpecial.k"));

        }
        static KeyValuePair<string, Object> Create(string filename)
        {
            using (var stream = new System.IO.StreamReader(filename))
                return new KeyValuePair<string, Object>(stream.ReadLine().Substring(1), Primitives
                                                        .Get("eval")
                                                        .Evaluate(new String(stream.ReadToEnd()),
                                                                  Environment.Ground));
        }
        public static Object Get(string name)
        => data.ContainsKey(name) ? data[name] : throw new NoBindingException("No such primitive");
        public static bool Has(string name)
        => data.ContainsKey(name);

    }
}
