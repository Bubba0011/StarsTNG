namespace Stars.Core.Views
{
	class OldScannedPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement { get; }
		public string ObjectId => planet.ObjectId;
		public StarDate? Timestamp { get; }

		public OldScannedPlanet(Planet planet, PlanetRecord history)
		{
			this.planet = planet;
			this.Timestamp = history.Time;

			if (history.OwnerId.HasValue)
			{
				Settlement = new OldSettlement(history);
			}
		}

		class OldSettlement : ISettlement
		{
			private readonly PlanetRecord history;

			public OldSettlement(PlanetRecord history)
			{
				this.history = history;
			}

			public int OwnerId => history.OwnerId!.Value;
			public int ScannerRange => history.ScannerRange!.Value;
			public Population Population => history.Population!.Value;
			public bool IsMine => false;
		}
	}
}
