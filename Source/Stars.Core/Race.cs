namespace Stars.Core
{
	public class Race
	{
		public int PlanetPopulationCapacity { get; set; } = 1_000_000;
		public double PopulationGrowthRate { get; set; } = 0.14;
		public Environment EnvironmentPreferences { get; set; } = new Environment(50, 50, 50);
	}
}
