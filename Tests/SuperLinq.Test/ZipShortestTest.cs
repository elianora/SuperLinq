﻿namespace Test;

public class ZipShortestTest
{
	[Fact]
	public void BothSequencesDisposedWithUnequalLengthsAndLongerFirst()
	{
		using var longer = TestingSequence.Of(1, 2, 3);
		using var shorter = TestingSequence.Of(1, 2);

		longer.ZipShortest(shorter, (x, y) => x + y).Consume();
	}

	[Fact]
	public void BothSequencesDisposedWithUnequalLengthsAndShorterFirst()
	{
		using var longer = TestingSequence.Of(1, 2, 3);
		using var shorter = TestingSequence.Of(1, 2);

		shorter.ZipShortest(longer, (x, y) => x + y).Consume();
	}

	[Fact]
	public void ZipShortestWithEqualLengthSequences()
	{
		var zipped = new[] { 1, 2, 3 }.ZipShortest(new[] { 4, 5, 6 }, ValueTuple.Create);
		Assert.NotNull(zipped);
		zipped.AssertSequenceEqual((1, 4), (2, 5), (3, 6));
	}

	[Fact]
	public void ZipShortestWithFirstSequenceShorterThanSecond()
	{
		var zipped = new[] { 1, 2 }.ZipShortest(new[] { 4, 5, 6 }, ValueTuple.Create);
		Assert.NotNull(zipped);
		zipped.AssertSequenceEqual((1, 4), (2, 5));
	}

	[Fact]
	public void ZipShortestWithFirstSequnceLongerThanSecond()
	{
		var zipped = new[] { 1, 2, 3 }.ZipShortest(new[] { 4, 5 }, ValueTuple.Create);
		Assert.NotNull(zipped);
		zipped.AssertSequenceEqual((1, 4), (2, 5));
	}

	[Fact]
	public void ZipShortestIsLazy()
	{
		var bs = new BreakingSequence<int>();
		bs.ZipShortest(bs, BreakingFunc.Of<int, int, int>());
	}

	[Fact]
	public void MoveNextIsNotCalledUnnecessarilyWhenFirstIsShorter()
	{
		using var s1 = TestingSequence.Of(1, 2);
		using var s2 = SuperEnumerable.From(() => 4,
										   () => 5,
										   () => throw new TestException())
									 .AsTestingSequence();

		var zipped = s1.ZipShortest(s2, ValueTuple.Create);

		Assert.NotNull(zipped);
		zipped.AssertSequenceEqual((1, 4), (2, 5));
	}

	[Fact]
	public void ZipShortestNotIterateUnnecessaryElements()
	{
		using var s1 = SuperEnumerable.From(() => 4,
										   () => 5,
										   () => 6,
										   () => throw new TestException())
									 .AsTestingSequence();
		using var s2 = TestingSequence.Of(1, 2);

		var zipped = s1.ZipShortest(s2, ValueTuple.Create);

		Assert.NotNull(zipped);
		zipped.AssertSequenceEqual((4, 1), (5, 2));
	}

	[Fact]
	public void ZipShortestDisposesInnerSequencesCaseGetEnumeratorThrows()
	{
		using var s1 = TestingSequence.Of(1, 2);

		Assert.Throws<InvalidOperationException>(() =>
			s1.ZipShortest(new BreakingSequence<int>(), ValueTuple.Create).Consume());
	}
}
