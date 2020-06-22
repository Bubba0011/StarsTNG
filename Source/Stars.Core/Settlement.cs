namespace Stars.Core
{
	public class Settlement
	{
		public int OwnerId { get; set; }
		public int ScannerRange { get; set; }
		public Population Population { get; set; }
		public BuildQueue BuildQueue { get; set; } = new BuildQueue();

		public ISettlement GetDefaultView() => new DefaultSettlementView(this);
	}

	public struct Population
	{
		public int Civilians { get; set; }
		public int Marines { get; set; }

		public int Total => Civilians + Marines;

		public Population(int civilians, int marines = 0)
		{
			Civilians = civilians;
			Marines = marines;
		}

		public override string ToString() => $"{Civilians}+{Marines}";

		public static Population operator +(Population lhs, Population rhs)
		{
			return new Population(lhs.Civilians + rhs.Civilians, lhs.Marines + rhs.Marines);
		}

		public static Population operator -(Population lhs, Population rhs)
		{
			return new Population(lhs.Civilians - rhs.Civilians, lhs.Marines - rhs.Marines);
		}
	}
}
