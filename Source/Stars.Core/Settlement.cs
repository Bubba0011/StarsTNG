namespace Stars.Core
{
	public class Settlement : ISettlement
	{
		public int OwnerId { get; set; }
		public int ScannerRange { get; set; }
		public int Population { get; set; }
	}
}
