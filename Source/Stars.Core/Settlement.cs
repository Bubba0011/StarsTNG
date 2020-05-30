namespace Stars.Core
{
	public class Settlement
	{
		public int OwnerId { get; set; }
		public int ScannerRange { get; set; }
		public int Population { get; set; }
		public BuildQueue BuildQueue { get; set; } = new BuildQueue();

		public ISettlement GetDefaultView() => new DefaultSettlementView(this);
	}
}
