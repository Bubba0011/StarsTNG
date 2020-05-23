namespace Stars.Core
{
	public interface IFleet : ISpaceObject
	{
		int Id { get; }
		int OwnerId { get; }
		string? Name { get; }
		int ScannerRange { get; }
		bool IsMine { get; }
	}
}
