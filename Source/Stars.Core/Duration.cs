using System;

namespace Stars.Core
{
	public struct Duration
	{
		public int Ticks { get; set; }
		public int Years => Ticks / StarDate.TicksPerYear;
		public int Months => (Ticks % StarDate.TicksPerYear) / StarDate.TicksPerMonth;
		public double YearFraction => (double)Ticks / StarDate.TicksPerYear;

		public Duration(int years, int months = 0)
		{
			Ticks = StarDate.TicksPerYear * years + StarDate.TicksPerMonth * months;
		}

		public override string ToString()
		{
			return $"{Years}.{Months}";
		}

		public static Duration FromYears(double years)
		{
			return new Duration
			{
				Ticks = (int)Math.Ceiling(StarDate.TicksPerYear * years),
			};
		}

		public static Duration operator +(Duration lhs, Duration rhs)
		{
			return new Duration
			{
				Ticks = lhs.Ticks + rhs.Ticks,
			};
		}
	}
}
