namespace Stars.Core
{
	class DefaultSettlementView : ISettlement
	{
		private readonly Settlement settlement;

		public int OwnerId => settlement.OwnerId;
		public int ScannerRange => settlement.Installations.Scanner;
		public Population Population => settlement.Population;
		public bool IsMine => false;

		public DefaultSettlementView(Settlement settlement)
		{
			this.settlement = settlement;
		}
	}
}
