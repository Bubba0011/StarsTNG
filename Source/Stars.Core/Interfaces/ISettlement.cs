namespace Stars.Core
{
	public interface ISettlement
	{
		public int OwnerId { get; }
		public int ScannerRange { get; }
		public Population Population { get; }
		public bool IsMine { get; }
		public Installations? Installations { get => null; }
	}
}
