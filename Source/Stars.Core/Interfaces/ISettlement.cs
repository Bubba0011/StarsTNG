namespace Stars.Core
{
	public interface ISettlement
	{
		public int OwnerId { get; }
		public int ScannerRange { get; }
		public int Population { get; }
	}
}
