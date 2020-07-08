namespace Stars.Core.Views
{
	class ScannedPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement { get; }
		public string ObjectId => planet.ObjectId;

		public ScannedPlanet(Planet planet)
		{
			this.planet = planet;

			if (planet.Settlement != null)
			{
				Settlement = new SettlementView(planet.Settlement);
			}
		}
	}
}
