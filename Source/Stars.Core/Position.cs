namespace Stars.Core
{
	public struct Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"{X},{Y}";
		public override bool Equals(object obj)
		{
			if (!(obj is Position)) return false;
			{
				Position p = (Position)obj;
				return (X == p.X) && (Y == p.Y);
			}
		}

		public override int GetHashCode()
		{
			return X ^ Y;
		}
	}
}
