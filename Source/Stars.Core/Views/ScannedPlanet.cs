namespace Stars.Core.Views
{
	class ScannedPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement => planet.Settlement?.GetDefaultView();
		public string ObjectId => planet.ObjectId;

		public ScannedPlanet(Planet planet)
		{
			this.planet = planet;
		}
	}
}
