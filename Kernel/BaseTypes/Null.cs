﻿using System;
namespace Kernel
{
	public sealed class Null : List
	{
		public static readonly Null Instance = new Null();

		Null()
		{
		}

		public override bool IsCyclic => false;

		public override List EvaluateAll(Environment environment) => this;

		public override Object this[int index] => throw new InvalidOperationException("Empty list cannot be indexed");

		public override string ToString() => "()";

		public override bool Equals(Object other) => other is Null;

	}
}
