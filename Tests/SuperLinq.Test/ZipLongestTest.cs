﻿namespace Test;
public class ZipLongestTest
{
	static IEnumerable<T> Seq<T>(params T[] values) => values;

	public static readonly IEnumerable<object[]> TestData =
		new[]
		{
			new object[] { Seq<int>(  ), Seq("foo", "bar", "baz"), Seq((0, "foo"), (0, "bar"), (0, "baz")) },
			new object[] { Seq(1      ), Seq("foo", "bar", "baz"), Seq((1, "foo"), (0, "bar"), (0, "baz")) },
			new object[] { Seq(1, 2   ), Seq("foo", "bar", "baz"), Seq((1, "foo"), (2, "bar"), (0, "baz")) },
			new object[] { Seq(1, 2, 3), Seq<string>(           ), Seq((1, null ), (2, null ), (3, (string) null)) },
			new object[] { Seq(1, 2, 3), Seq("foo"              ), Seq((1, "foo"), (2, null ), (3, null )) },
			new object[] { Seq(1, 2, 3), Seq("foo", "bar"       ), Seq((1, "foo"), (2, "bar"), (3, null )) },
			new object[] { Seq(1, 2, 3), Seq("foo", "bar", "baz"), Seq((1, "foo"), (2, "bar"), (3, "baz")) },
		};


	[Theory]
	[MemberData(nameof(TestData))]
	public void ZipLongest(int[] first, string[] second, IEnumerable<(int, string)> expected)
	{
		using var ts1 = TestingSequence.Of(first);
		using var ts2 = TestingSequence.Of(second);
		Assert.Equal(expected, ts1.ZipLongest(ts2, ValueTuple.Create).ToArray());
	}

	[Fact]
	public void ZipLongestIsLazy()
	{
		var bs = new BreakingSequence<int>();
		bs.ZipLongest(bs, BreakingFunc.Of<int, int, int>());
	}

	[Fact]
	public void ZipLongestDisposeSequencesEagerly()
	{
		var shorter = TestingSequence.Of(1, 2, 3);
		var longer = SuperEnumerable.Generate(1, x => x + 1);
		var zipped = shorter.ZipLongest(longer, ValueTuple.Create);

		var count = 0;
		foreach (var _ in zipped.Take(10))
		{
			if (++count == 4)
				((IDisposable)shorter).Dispose();
		}
	}

	[Fact]
	public void ZipLongestDisposesInnerSequencesCaseGetEnumeratorThrows()
	{
		using var s1 = TestingSequence.Of(1, 2);

		Assert.Throws<InvalidOperationException>(() =>
			s1.ZipLongest(new BreakingSequence<int>(), ValueTuple.Create).Consume());
	}
}
