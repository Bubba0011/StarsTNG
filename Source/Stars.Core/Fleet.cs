namespace Stars.Core
{
	public class Fleet : ISpaceObject
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public Position Position { get; set; }
		public string? Name { get; set; }
		public int ScannerRange { get; set; }

		public IFleet GetDefaultView() => new DefaultFleetView(this);
	}
}
