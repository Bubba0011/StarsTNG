using System;

namespace Stars.Core
{
	public struct Position : IEquatable<Position>
	{
		public static readonly Position Zero = new Position();

		public int X { get; set; }
		public int Y { get; set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public double DistanceTo(Position position)
		{
			var dx = position.X - X;
			var dy = position.Y - Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		public override string ToString() => $"{X},{Y}";

		public override bool Equals(object? obj)
		{
			if (obj is Position other)
			{
				return EqualImpl(this, other);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public bool Equals(Position other)
		{
			return EqualImpl(this, other);
		}

		public static bool operator ==(Position lhs, Position rhs)
		{
			return EqualImpl(lhs, rhs);
		}

		public static bool operator !=(Position lhs, Position rhs)
		{
			return !EqualImpl(lhs, rhs);
		}

		private static bool EqualImpl(Position lhs, Position rhs)
		{
			return lhs.X == rhs.X && lhs.Y == rhs.Y;
		}

		public static Position operator +(Position lhs, Position rhs)
		{
			var dx = lhs.X + rhs.X;
			var dy = lhs.Y + rhs.Y;
			return new Position(dx, dy);
		}

		public static Position operator -(Position lhs, Position rhs)
		{
			var dx = lhs.X - rhs.X;
			var dy = lhs.Y - rhs.Y;
			return new Position(dx, dy);
		}

		public static Position operator *(double lhs, Position rhs)
		{
			return rhs * lhs;
		}

		public static Position operator *(Position lhs, double rhs)
		{
			static int Round(double value) => (int)value;

			var x = Round(lhs.X * rhs);
			var y = Round(lhs.Y * rhs);
			return new Position(x, y);
		}
	}
}
