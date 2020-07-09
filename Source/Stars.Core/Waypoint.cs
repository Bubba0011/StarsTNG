namespace Stars.Core
{
	public struct Waypoint
	{
		public Position Position { get; set; }

		public Waypoint(Position position)
		{
			Position = position;
		}
	}
}
