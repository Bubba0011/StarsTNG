﻿namespace Stars.Core
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
	}
}
