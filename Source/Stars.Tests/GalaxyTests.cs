using Stars.Core;
using Stars.Core.Setup;
using System;
using System.Linq;
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
		[InlineData(800, 100, 15)]
		public void GalaxyGeneratorWorks(int size, int planets, int minDistance)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
				MinimumDistanceBetweenPlanets = minDistance
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(planets, galaxy.Planets.Count);
			Assert.Equal(size, galaxy.Size);
			Assert.All(galaxy.Planets, planet => Assert.True(PointInRange(planet.Position)));
			Assert.All(galaxy.Planets, planet => Assert.True(MinDistanceKept(planet)));
			Assert.Equal(planets, galaxy.Planets.Select(planet => planet.Position).Distinct().Count());

			bool CoordInRange(int coord) => coord >= 0 && coord < size;
			bool PointInRange(Position position) => CoordInRange(position.X) && CoordInRange(position.Y);

			bool PassesSocialDistancingRule(Planet p1, Planet p2) => p1 != p2 ? p1.Position.DistanceTo(p2.Position) >= minDistance : true;
			bool MinDistanceKept(Planet planet) => galaxy.Planets.All(otherPlanet => PassesSocialDistancingRule(planet, otherPlanet));
		}
	}
}
