using System;
using System.Collections;
using System.Collections.Generic;
namespace Kernel
{
	public sealed class Null : Object, IEnumerable<Object>
	{
		public static readonly Null Instance = new Null();

		Null()
		{
		}

		public IEnumerator<Object> GetEnumerator() => new NullEnumerator();

		public override string ToString() => "()";

		IEnumerator IEnumerable.GetEnumerator() => new NullEnumerator();

		class NullEnumerator : IEnumerator<Object>
		{
			public Object Current => throw new InvalidOperationException("Wtf?");

			object IEnumerator.Current => throw new InvalidOperationException("Wtf");

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
