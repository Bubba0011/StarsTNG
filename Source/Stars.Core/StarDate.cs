using System;

namespace Stars.Core
{
	public struct StarDate : IComparable<StarDate>
	{
		const int BaseYear = 1;

		internal const int TicksPerYear = 10;
		internal const int TicksPerMonth = 1;

		public int Ticks { get; set; }
		public int Year => BaseYear + Ticks / TicksPerYear;
		public int Month => (Ticks % TicksPerYear) / TicksPerMonth + 1;

		public StarDate(int year, int month = 1)
		{
			Ticks = TicksPerYear * (year - BaseYear) + TicksPerMonth * (month - 1);
		}

		public override string ToString()
		{
			return $"{Year}.{Month}";
		}

		public override bool Equals(object? obj)
		{
			if (obj is StarDate st)
			{
				return st == this;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Ticks);
		}

		int IComparable<StarDate>.CompareTo(StarDate other)
		{
			return Ticks.CompareTo(other.Ticks);
		}

		public static StarDate operator+(StarDate lhs, Duration rhs)
		{
			return new StarDate
			{
				Ticks = lhs.Ticks + rhs.Ticks
			};
		}

		public static bool operator ==(StarDate lhs, StarDate rhs)
		{
			return lhs.Ticks == rhs.Ticks;
		}

		public static bool operator !=(StarDate lhs, StarDate rhs)
		{
			return lhs.Ticks != rhs.Ticks;
		}
	}

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
	}
}
