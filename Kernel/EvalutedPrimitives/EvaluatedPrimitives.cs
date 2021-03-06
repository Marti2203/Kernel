﻿using System.IO;
using System.Linq;
using System.Collections.Generic;
using static Kernel.Primitives.Primitives;
using Evaluation = System.Collections.Generic.KeyValuePair<string, Kernel.BaseTypes.Object>;
using Kernel.BaseTypes;

namespace Kernel
{
    public static class EvaluatedPrimitives
    {
        static readonly IDictionary<string, Object> data = new Dictionary<string, Object>();
        static EvaluatedPrimitives()
        {
            if (Directory.Exists("./EvaluatedPrimes"))
                foreach (string path in Directory.GetFiles("./EvaluatedPrimitives")//Possibly
                         .Where(name => name.EndsWith(".k", System.StringComparison.Ordinal)))
                    data.Add(Create(path));
            data.Add("list-core", Evaluate(String.Get("($define! list (wrap ($vau x #ignore x)))"), Environment.Ground));
            data.Add("list-special", Evaluate(String.Get("($define! list ($lambda x x))"), Environment.Ground));

            data.Add("list-tail-core", Evaluate(String.Get("($define! list-tail ($lambda(ls k) ($if (>? k 0) (list-tail(cdr ls)(- k 1)) ls)))"), Environment.Ground));

            data.Add("encycle-core!", Evaluate
                     (String.Get("($define! encycle! ($lambda(ls k1 k2) ($if (>? k2 0) (set-cdr! (list-tail ls (+ k1 k2 -1)) (list-tail ls k1)) #inert)))"), Environment.Ground));

            data.Add("not-core?", Evaluate(String.Get("($define! not? ($lambda (x) ($if x #f #t)))"), Environment.Ground));

            data.Add("get-current-environment-core", Evaluate(String.Get("($define! get-current-environment (wrap ($vau () e e)))"), Environment.Ground));

            data.Add("make-kernel-standard-environment-core", Evaluate(String.Get("($define! make-kernel-standard-environment ($lambda()(get-current-environment)))"), Environment.Ground));

            data.Add("remote-eval-core", Evaluate(String.Get("($define! $remote-eval ($vau (o e) d (eval o(eval e d))))"), Environment.Ground));

            data.Add("for-each-core", Evaluate(String.Get("($define! for-each (wrap ($vau x env (apply map x env) #inert)))"), Environment.Ground));

        }
        static KeyValuePair<string, Object> Create(string filename)
        {
            using var stream = new StreamReader(filename);
            return new Evaluation(stream.ReadLine().Substring(1), Evaluate(String.Get(stream.ReadToEnd()), Environment.Ground));
        }
    }
}
