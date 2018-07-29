using System;
using System.Collections.Generic;
using static Kernel.Primitives.Primitives;
namespace Kernel
{
    public static class EvaluatedPrimitives
    {
        static readonly IDictionary<string, Object> data = new Dictionary<string, Object>();
        static EvaluatedPrimitives()
        {
            data.Add(Create("CopyEsImmutable.k"));
            data.Add(Create("LambdaCore.k"));

            data.Add("list-core", Evaluate(new String("($define! list (wrap ($vau x #ignore x)))"), Environment.Ground));
            data.Add("list-special", Evaluate(new String("($define! list ($lambda x x))"), Environment.Ground));

            data.Add("list-tail-core", Evaluate(new String("($define! list-tail ($lambda(ls k) ($if (>? k 0) (list-tail(cdr ls)(- k 1)) ls)))"), Environment.Ground));

            data.Add("encycle-core!", Evaluate
                     (new String("($define! encycle! ($lambda(ls k1 k2) ($if (>? k2 0) (set-cdr! (list-tail ls (+ k1 k2 -1)) (list-tail ls k1)) #inert)))"), Environment.Ground));

            data.Add(Create("SequenceCore.k"));
            data.Add(Create("SequenceSpecial.k"));

            data.Add(Create("ListStarCore.k"));
            data.Add(Create("ListStarSpecial.k"));

            data.Add(Create("VauCore.k"));
            data.Add(Create("VauSpecial.k"));

            data.Add(Create("CondCore.k"));
            data.Add(Create("CondSpecial.k"));

        }
        static KeyValuePair<string, Object> Create(string filename)
        {
            using (var stream = new System.IO.StreamReader(filename))
                return new KeyValuePair<string, Object>(stream.ReadLine().Substring(1), Evaluate(new String(stream.ReadToEnd()),
                                                                  Environment.Ground));
        }
    }
}
