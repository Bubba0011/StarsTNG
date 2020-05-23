namespace Stars.Core.Views
{
	class OwnedSettlement : ISettlement
	{
		private readonly Settlement settlement;

		public int OwnerId => settlement.OwnerId;
		public int ScannerRange => settlement.ScannerRange;
		public int Population => settlement.Population;
		public bool IsMine => true;

		public OwnedSettlement(Settlement settlement)
		{
			this.settlement = settlement;
		}
	}
}
