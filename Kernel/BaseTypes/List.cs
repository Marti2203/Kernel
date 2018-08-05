using System.Collections;
using System.Collections.Generic;
namespace Kernel
{
	public abstract class List : Object, IEnumerable<Object>
	{
		public abstract Object EvaluateAll(Environment environment);
		public abstract IEnumerator<Object> GetEnumerator();

		//#error NIKI THIS IS FUCKED UP TO THE MAX
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public abstract Object this[int index] { get; }
		public abstract bool IsCyclic { get; }
	}
}
