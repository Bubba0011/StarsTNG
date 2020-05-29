namespace Stars.Core
{
	public class Player
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public Race Race { get; set; } = new Race();

		public IPlayer GetDefaultView() => new DefaultPlayerView(this);
	}

	public class Race
	{
		public int PlanetPopulationCapacity { get; set; } = 1_000_000;
		public double PopulationGrowthRate { get; set; } = 0.14;
		public Environment EnvironmentPreferences { get; set; } = new Environment(50, 50, 50);
	}
}
