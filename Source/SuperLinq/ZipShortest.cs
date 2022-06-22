﻿namespace SuperLinq;

public static partial class SuperEnumerable
{
	/// <summary>
	/// Returns a projection of tuples, where each tuple contains the N-th
	/// element from each of the argument sequences. The resulting sequence
	/// is as short as the shortest input sequence.
	/// </summary>
	/// <typeparam name="TFirst">Type of elements in first sequence.</typeparam>
	/// <typeparam name="TSecond">Type of elements in second sequence.</typeparam>
	/// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
	/// <param name="first">The first sequence.</param>
	/// <param name="second">The second sequence.</param>
	/// <param name="resultSelector">
	/// Function to apply to each pair of elements.</param>
	/// <returns>
	/// A projection of tuples, where each tuple contains the N-th element
	/// from each of the argument sequences.</returns>
	/// <example>
	/// <code><![CDATA[
	/// var numbers = new[] { 1, 2, 3 };
	/// var letters = new[] { "A", "B", "C", "D" };
	/// var zipped = numbers.ZipShortest(letters, (n, l) => n + l);
	/// ]]></code>
	/// The <c>zipped</c> variable, when iterated over, will yield "1A", "2B", "3C", in turn.
	/// </example>
	/// <remarks>
	/// <para>
	/// If the input sequences are of different lengths, the result sequence
	/// is terminated as soon as the shortest input sequence is exhausted
	/// and remainder elements from the longer sequences are never consumed.
	/// </para>
	/// <para>
	/// This operator uses deferred execution and streams its results.</para>
	/// </remarks>

	public static IEnumerable<TResult> ZipShortest<TFirst, TSecond, TResult>(
		this IEnumerable<TFirst> first,
		IEnumerable<TSecond> second,
		Func<TFirst, TSecond, TResult> resultSelector)
	{
		first.ThrowIfNull();
		second.ThrowIfNull();
		resultSelector.ThrowIfNull();

		return ZipImpl<TFirst, TSecond, object, object, TResult>(first, second, null, null, (a, b, c, d) => resultSelector(a, b));
	}

	/// <summary>
	/// Returns a projection of tuples, where each tuple contains the N-th
	/// element from each of the argument sequences. The resulting sequence
	/// is as short as the shortest input sequence.
	/// </summary>
	/// <typeparam name="T1">Type of elements in first sequence.</typeparam>
	/// <typeparam name="T2">Type of elements in second sequence.</typeparam>
	/// <typeparam name="T3">Type of elements in third sequence.</typeparam>
	/// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
	/// <param name="first">First sequence</param>
	/// <param name="second">Second sequence</param>
	/// <param name="third">Third sequence</param>
	/// <param name="resultSelector">
	/// Function to apply to each triplet of elements.</param>
	/// <returns>
	/// A projection of tuples, where each tuple contains the N-th element
	/// from each of the argument sequences.</returns>
	/// <example>
	/// <code><![CDATA[
	/// var numbers = new[] { 1, 2, 3 };
	/// var letters = new[] { "A", "B", "C", "D" };
	/// var chars   = new[] { 'a', 'b', 'c', 'd', 'e' };
	/// var zipped  = numbers.ZipShortest(letters, chars, (n, l, c) => c + n + l);
	/// ]]></code>
	/// The <c>zipped</c> variable, when iterated over, will yield
	/// "98A", "100B", "102C", in turn.
	/// </example>
	/// <remarks>
	/// <para>
	/// If the input sequences are of different lengths, the result sequence
	/// is terminated as soon as the shortest input sequence is exhausted
	/// and remainder elements from the longer sequences are never consumed.
	/// </para>
	/// <para>
	/// This operator uses deferred execution and streams its results.</para>
	/// </remarks>

	public static IEnumerable<TResult> ZipShortest<T1, T2, T3, TResult>(
		this IEnumerable<T1> first,
		IEnumerable<T2> second,
		IEnumerable<T3> third,
		Func<T1, T2, T3, TResult> resultSelector)
	{
		first.ThrowIfNull();
		second.ThrowIfNull();
		third.ThrowIfNull();
		resultSelector.ThrowIfNull();

		return ZipImpl<T1, T2, T3, object, TResult>(first, second, third, null, (a, b, c, _) => resultSelector(a, b, c));
	}

	/// <summary>
	/// Returns a projection of tuples, where each tuple contains the N-th
	/// element from each of the argument sequences. The resulting sequence
	/// is as short as the shortest input sequence.
	/// </summary>
	/// <typeparam name="T1">Type of elements in first sequence.</typeparam>
	/// <typeparam name="T2">Type of elements in second sequence.</typeparam>
	/// <typeparam name="T3">Type of elements in third sequence.</typeparam>
	/// <typeparam name="T4">Type of elements in fourth sequence.</typeparam>
	/// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
	/// <param name="first">The first sequence.</param>
	/// <param name="second">The second sequence.</param>
	/// <param name="third">The third sequence.</param>
	/// <param name="fourth">The fourth sequence.</param>
	/// <param name="resultSelector">
	/// Function to apply to each quadruplet of elements.</param>
	/// <returns>
	/// A projection of tuples, where each tuple contains the N-th element
	/// from each of the argument sequences.</returns>
	/// <example>
	/// <code><![CDATA[
	/// var numbers = new[] { 1, 2, 3 };
	/// var letters = new[] { "A", "B", "C", "D" };
	/// var chars   = new[] { 'a', 'b', 'c', 'd', 'e' };
	/// var flags   = new[] { true, false };
	/// var zipped  = numbers.ZipShortest(letters, chars, flags, (n, l, c, f) => n + l + c + f);
	/// ]]></code>
	/// The <c>zipped</c> variable, when iterated over, will yield
	/// "1AaTrue", "2BbFalse" in turn.
	/// </example>
	/// <remarks>
	/// <para>
	/// If the input sequences are of different lengths, the result sequence
	/// is terminated as soon as the shortest input sequence is exhausted
	/// and remainder elements from the longer sequences are never consumed.
	/// </para>
	/// <para>
	/// This operator uses deferred execution and streams its results.</para>
	/// </remarks>

	public static IEnumerable<TResult> ZipShortest<T1, T2, T3, T4, TResult>(
		this IEnumerable<T1> first,
		IEnumerable<T2> second,
		IEnumerable<T3> third,
		IEnumerable<T4> fourth,
		Func<T1, T2, T3, T4, TResult> resultSelector)
	{
		first.ThrowIfNull();
		second.ThrowIfNull();
		third.ThrowIfNull();
		fourth.ThrowIfNull();
		resultSelector.ThrowIfNull();

		return ZipImpl(first, second, third, fourth, resultSelector);
	}

	static IEnumerable<TResult> ZipImpl<T1, T2, T3, T4, TResult>(
		IEnumerable<T1> s1,
		IEnumerable<T2> s2,
		IEnumerable<T3>? s3,
		IEnumerable<T4>? s4,
		Func<T1, T2, T3, T4, TResult> resultSelector)
	{
		return ZipImpl(s1, s2, s3, s4, resultSelector, 0);
	}
}