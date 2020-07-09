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
		public int Month => (Ticks % TicksPerYear) / TicksPerMonth;

		public StarDate(int year, int month = 0)
		{
			Ticks = TicksPerYear * (year - BaseYear) + TicksPerMonth * month;
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
			return Ticks;
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
}
