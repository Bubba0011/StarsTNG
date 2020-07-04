namespace Stars.Core
{
	public class Settlement
	{
		public int OwnerId { get; set; }
		public Population Population { get; set; }
		public BuildQueue BuildQueue { get; set; } = new BuildQueue();
		public Installations Installations { get; set; } = new Installations();

		public ISettlement GetDefaultView() => new DefaultSettlementView(this);
	}

	public class Installations
	{
		public int Scanner { get; set; }
		public int SpacePort { get; set; }
	}
}
