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

		public bool Equals(Position other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public static bool operator ==(Position lhs, Position rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Position lhs, Position rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}
