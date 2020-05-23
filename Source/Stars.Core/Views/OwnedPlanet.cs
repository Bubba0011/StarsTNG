namespace Stars.Core.Views
{
	class OwnedPlanet : IPlanet
	{
		private readonly Planet planet;
		private readonly OwnedSettlement settlement;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement => settlement;
		public string ObjectId => planet.ObjectId;

		public OwnedPlanet(Planet planet)
		{
			this.planet = planet;
			settlement = new OwnedSettlement(planet.Settlement!);
		}
	}
}
