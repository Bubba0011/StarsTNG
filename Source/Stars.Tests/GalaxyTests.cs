using Stars.Core;
using Stars.Core.Setup;
using System;
using Xunit;

namespace Stars.Tests
{
	public class GalaxyTests
	{
		[Fact]
		public void PlanetsShouldNeverBeNull()
		{
			var galaxy = new Galaxy();

			Assert.NotNull(galaxy.Planets);
		}

		[Theory]
		[InlineData(800, 100)]
		public void GalaxyGeneratorWorks(int size, int planets)
		{
			var generator = new GalaxyGenerator();
			
			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(planets, galaxy.Planets.Count);

			Assert.All(galaxy.Planets, planet => Assert.True(planet.Position.X >= 0));
			Assert.All(galaxy.Planets, planet => Assert.True(planet.Position.Y >= 0));
			Assert.All(galaxy.Planets, planet => Assert.True(planet.Position.X < size));
			Assert.All(galaxy.Planets, planet => Assert.True(planet.Position.Y < size));
		}
	}
}
