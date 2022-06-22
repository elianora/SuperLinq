﻿namespace SuperLinq;

public static partial class SuperEnumerable
{
	/// <summary>
	/// Determines whether the end of the first sequence is equivalent to
	/// the second sequence, using the default equality comparer.
	/// </summary>
	/// <typeparam name="T">Type of elements.</typeparam>
	/// <param name="first">The sequence to check.</param>
	/// <param name="second">The sequence to compare to.</param>
	/// <returns>
	/// <c>true</c> if <paramref name="first" /> ends with elements
	/// equivalent to <paramref name="second" />.
	/// </returns>
	/// <remarks>
	/// This is the <see cref="IEnumerable{T}" /> equivalent of
	/// <see cref="string.EndsWith(string)" /> and
	/// it calls <see cref="IEqualityComparer{T}.Equals(T,T)" /> using
	/// <see cref="EqualityComparer{T}.Default" /> on pairs of elements at
	/// the same index.
	/// </remarks>

	public static bool EndsWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
	{
		return EndsWith(first, second, null);
	}

	/// <summary>
	/// Determines whether the end of the first sequence is equivalent to
	/// the second sequence, using the specified element equality comparer.
	/// </summary>
	/// <typeparam name="T">Type of elements.</typeparam>
	/// <param name="first">The sequence to check.</param>
	/// <param name="second">The sequence to compare to.</param>
	/// <param name="comparer">Equality comparer to use.</param>
	/// <returns>
	/// <c>true</c> if <paramref name="first" /> ends with elements
	/// equivalent to <paramref name="second" />.
	/// </returns>
	/// <remarks>
	/// This is the <see cref="IEnumerable{T}" /> equivalent of
	/// <see cref="string.EndsWith(string)" /> and it calls
	/// <see cref="IEqualityComparer{T}.Equals(T,T)" /> on pairs of
	/// elements at the same index.
	/// </remarks>

	public static bool EndsWith<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T>? comparer)
	{
		first.ThrowIfNull();
		second.ThrowIfNull();

		comparer ??= EqualityComparer<T>.Default;

		List<T> secondList;
		return second.TryGetCollectionCount(out var secondCount)
			   ? first.TryGetCollectionCount(out var firstCount) && secondCount > firstCount
				 ? false
				 : Impl(second, secondCount)
			   : Impl(secondList = second.ToList(), secondList.Count);

		bool Impl(IEnumerable<T> snd, int count)
		{
			using var firstIter = first.TakeLast(count).GetEnumerator();
			return snd.All(item => firstIter.MoveNext() && comparer.Equals(firstIter.Current, item));
		}
	}
}