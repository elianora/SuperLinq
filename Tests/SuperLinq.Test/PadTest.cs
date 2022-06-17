﻿namespace Test;

public class PadTest
{
	[Fact]
	public void PadNegativeWidth()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() =>
			Array.Empty<object>().Pad(-1));
	}

	[Fact]
	public void PadIsLazy()
	{
		new BreakingSequence<object>().Pad(0);
	}

	[Fact]
	public void PadWithFillerIsLazy()
	{
		new BreakingSequence<object>().Pad(0, new object());
	}

	public class ValueTypeElements
	{
		[Fact]
		public void PadWideSourceSequence()
		{
			var result = new[] { 123, 456, 789 }.Pad(2);
			result.AssertSequenceEqual(123, 456, 789);
		}

		[Fact]
		public void PadEqualSourceSequence()
		{
			var result = new[] { 123, 456, 789 }.Pad(3);
			result.AssertSequenceEqual(123, 456, 789);
		}

		[Fact]
		public void PadNarrowSourceSequenceWithDefaultPadding()
		{
			var result = new[] { 123, 456, 789 }.Pad(5);
			result.AssertSequenceEqual(123, 456, 789, 0, 0);
		}

		[Fact]
		public void PadNarrowSourceSequenceWithNonDefaultPadding()
		{
			var result = new[] { 123, 456, 789 }.Pad(5, -1);
			result.AssertSequenceEqual(123, 456, 789, -1, -1);
		}

		[Fact]
		public void PadNarrowSourceSequenceWithDynamicPadding()
		{
			var result = "hello".ToCharArray().Pad(15, i => i % 2 == 0 ? '+' : '-');
			result.AssertSequenceEqual("hello-+-+-+-+-+".ToCharArray());
		}
	}

	public class ReferenceTypeElements
	{
		[Fact]
		public void PadWideSourceSequence()
		{
			var result = new[] { "foo", "bar", "baz" }.Pad(2);
			result.AssertSequenceEqual("foo", "bar", "baz");
		}

		[Fact]
		public void PadEqualSourceSequence()
		{
			var result = new[] { "foo", "bar", "baz" }.Pad(3);
			result.AssertSequenceEqual("foo", "bar", "baz");
		}

		[Fact]
		public void PadNarrowSourceSequenceWithDefaultPadding()
		{
			var result = new[] { "foo", "bar", "baz" }.Pad(5);
			result.AssertSequenceEqual("foo", "bar", "baz", null, null);
		}

		[Fact]
		public void PadNarrowSourceSequenceWithNonDefaultPadding()
		{
			var result = new[] { "foo", "bar", "baz" }.Pad(5, string.Empty);
			result.AssertSequenceEqual("foo", "bar", "baz", string.Empty, string.Empty);
		}
	}
}
