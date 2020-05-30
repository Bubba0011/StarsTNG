namespace Stars.Core
{
	public interface ISettlementController : ISettlement
	{
		public BuildQueue BuildQueue { get; }
	}
}
