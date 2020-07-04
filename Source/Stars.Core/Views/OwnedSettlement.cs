namespace Stars.Core.Views
{
	class OwnedSettlement : ISettlementController
	{
		private readonly Settlement settlement;

		public int OwnerId => settlement.OwnerId;
		public int ScannerRange => settlement.Installations.Scanner;
		public Population Population => settlement.Population;
		public bool IsMine => true;
		public Installations Installations => settlement.Installations;

		public BuildQueue BuildQueue => settlement.BuildQueue;

		public OwnedSettlement(Settlement settlement)
		{
			this.settlement = settlement;
		}
	}
}
