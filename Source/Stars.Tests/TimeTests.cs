﻿using Stars.Core;
using Xunit;

namespace Stars.Tests
{
	public class TimeTests
	{
		[Fact]
		public void TimeStartsAtYearZero()
		{
			var startYear = new StarDate();
			var yearZero = new StarDate(0, 0);

			Assert.Equal(yearZero, startYear);
		}

		[Fact]
		public void XPlusZeroEqualsX()
		{
			var start = new StarDate();
			var zero = new Duration(0, 0);

			StarDate same = start + zero;

			Assert.Equal(start, same);
		}

		[Fact]
		public void XPlusNonZeroIsNotX()
		{
			var start = new StarDate();
			var one = new Duration(0, 1);

			StarDate future = start + one;

			Assert.NotEqual(start, future);
		}

		[Fact]
		public void ItAddsUp()
		{
			var start = new StarDate();
			var delta = new Duration(0, 2);
			var expected = new StarDate(0, 2);

			StarDate future = start + delta;

			Assert.Equal(expected, future);
		}

		[Fact]
		public void YearHasTenMonths()
		{
			var start = new StarDate(1, 9);
			var oneMonth = new Duration(0, 1);
			var expected = new StarDate(2, 0);

			StarDate future = start + oneMonth;

			Assert.Equal(expected, future);
		}
	}
}
