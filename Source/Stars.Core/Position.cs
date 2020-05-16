using System;

namespace Stars.Core
{
	public struct Position : IEquatable<Position>
	{
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
	}
}
