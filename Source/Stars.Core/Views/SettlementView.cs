namespace Stars.Core
{
	class SettlementView : ISettlement
	{
		private readonly Settlement settlement;

		public int OwnerId => settlement.OwnerId;
		public int ScannerRange => settlement.Installations.Scanner;
		public Population Population => settlement.Population;
		public bool IsMine => false;

		public SettlementView(Settlement settlement)
		{
			this.settlement = settlement;
		}
	}
}
