namespace Stars.Core.Views
{
	class UnknowPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => null;
		public ISettlement? Settlement => null; 

		public UnknowPlanet(Planet planet)
		{
			this.planet = planet;
		}
	}
}
