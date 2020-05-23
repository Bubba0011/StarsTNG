namespace Stars.Core.Views
{
	struct ScannerSite
	{
		public Position Position { get; }
		public double Range { get; }

		public bool InRange(Position position) => Position.DistanceTo(position) <= Range;

		public ScannerSite(Position pos, double range)
		{
			Position = pos;
			Range = range;
		}
	}
}
