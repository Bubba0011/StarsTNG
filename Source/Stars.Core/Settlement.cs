namespace Stars.Core
{
	public class Settlement
	{
		public int OwnerId { get; set; }
		public int ScannerRange { get; set; }
		public int Population { get; set; }

		public ISettlement GetDefaultView() => new DefaultSettlementView(this);
	}
}
