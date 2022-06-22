﻿using System.Collections;

namespace Test;

/// <summary>
/// Enumerable sequence which throws InvalidOperationException as soon as its
/// enumerator is requested. Used to check lazy evaluation.
/// </summary>
class BreakingSequence<T> : IEnumerable<T>
{
	public IEnumerator<T> GetEnumerator() => throw new InvalidOperationException();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}