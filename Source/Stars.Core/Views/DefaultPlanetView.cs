namespace Stars.Core
{
	class DefaultPlanetView : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement => planet.Settlement?.GetDefaultView();

		public DefaultPlanetView(Planet planet)
		{
			this.planet = planet;
		}
	}
}
