using System;

namespace Stars.Core
{
	public struct SpaceTime : IComparable<SpaceTime>
	{
		const int BaseYear = 3999;

		public int Year { get; set; }

		public int Ticks => Year;

		public SpaceTime(int year)
		{
			Year = year;
		}

		public override string ToString()
		{
			return $"{BaseYear + Year}";
		}

		public override bool Equals(object? obj)
		{
			if (obj is SpaceTime st)
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

		int IComparable<SpaceTime>.CompareTo(SpaceTime other)
		{
			return Ticks.CompareTo(other.Ticks);
		}

		public static SpaceTime operator+(SpaceTime lhs, SpaceTimeSpan rhs)
		{
			return new SpaceTime(lhs.Year + rhs.Years);
		}

		public static bool operator ==(SpaceTime lhs, SpaceTime rhs)
		{
			return lhs.Ticks == rhs.Ticks;
		}

		public static bool operator !=(SpaceTime lhs, SpaceTime rhs)
		{
			return lhs.Ticks != rhs.Ticks;
		}
	}

	public struct SpaceTimeSpan
	{
		public int Years { get; set; }

		public SpaceTimeSpan(int years)
		{
			Years = years;
		}
	}
}
