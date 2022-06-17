﻿namespace Test;
public class PreScanTest
{
	[Fact]
	public void PreScanIsLazy()
	{
		new BreakingSequence<int>().PreScan(BreakingFunc.Of<int, int, int>(), 0);
	}

	[Fact]
	public void PreScanWithEmptySequence()
	{
		var source = Enumerable.Empty<int>();
		var result = source.PreScan(BreakingFunc.Of<int, int, int>(), 0);

		Assert.Empty(result);
	}

	[Fact]
	public void PreScanWithSingleElement()
	{
		var source = new[] { 111 };
		var result = source.PreScan(BreakingFunc.Of<int, int, int>(), 999);
		result.AssertSequenceEqual(999);
	}

	[Fact]
	public void PreScanSum()
	{
		var result = SampleData.Values.PreScan(SampleData.Plus, 0);
		result.AssertSequenceEqual(0, 1, 3, 6, 10, 15, 21, 28, 36, 45);
	}

	[Fact]
	public void PreScanMul()
	{
		var seq = new[] { 1, 2, 3 };
		var result = seq.PreScan(SampleData.Mul, 1);
		result.AssertSequenceEqual(1, 1, 2);
	}

	[Fact]
	public void PreScanFuncIsNotInvokedUnnecessarily()
	{
		var count = 0;
		var gold = new[] { 0, 1, 3 };
		var sequence = Enumerable.Range(1, 3).PreScan((a, b) =>
			++count == gold.Length ? throw new TestException() : a + b, 0);

		sequence.AssertSequenceEqual(gold);
	}
}
