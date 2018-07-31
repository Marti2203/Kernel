using System;
using System.Collections;
using System.Collections.Generic;
namespace Kernel
{
	public sealed class Null : List
	{
		public static readonly Null Instance = new Null();

		Null()
		{
		}

        public override bool IsCyclic => false;

        public override Object EvaluateAll(Environment environment) => this;

        public override Object this[int index] => throw new InvalidOperationException("Empty list cannot be indexed");

        public override IEnumerator<Object> GetEnumerator() => new NullEnumerator();

        public override string ToString() => "()";

        class NullEnumerator : IEnumerator<Object>
        {
            public Object Current => throw new InvalidOperationException("Empty list cannot be enumerated.");

            object IEnumerator.Current => throw new InvalidOperationException("Empty list cannot be enumerated.");

            public void Dispose()
            {
            }

            public bool MoveNext() => false;

            public void Reset()
            {
            }
        }
	}
}
