namespace Stars.Core.Setup
{
	public class GalaxyGeneratorSettings
	{
		public int GalaxySize { get; set; } = 800;
		public int PlanetCount { get; set; } = 100;
		public int MinimumDistanceBetweenPlanets { get; set; } = 10;
		public int Padding { get; set; } = 10;
	}
}
