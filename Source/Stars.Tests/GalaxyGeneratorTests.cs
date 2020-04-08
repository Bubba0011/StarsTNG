using Stars.Core;
using Stars.Core.Setup;
using System;
using System.Linq;
using Xunit;

namespace Stars.Tests
{
	public class GalaxyGeneratorTests
	{
		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		[InlineData(500)]
		public void GalaxyHasCorrectNumberOfPlanets(int planets)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = 1000,
				PlanetCount = planets,
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(planets, galaxy.Planets.Count);
		}

		[Theory]
		[InlineData(100)]
		[InlineData(800)]
		[InlineData(50_000)]
		public void GalaxyHasCorrectSize(int size)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = 1,
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(size, galaxy.Size);
		}

		[Theory]
		[InlineData(100, 25)]
		[InlineData(800, 125)]
		[InlineData(2000, 500)]
		public void PlanetsHaveValidPositions(int size, int planets)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
			};

			var galaxy = generator.Generate(settings);

			Assert.All(galaxy.Planets, planet => Assert.True(PointInRange(planet.Position)));

			bool CoordInRange(int coord) => coord >= 0 && coord < size;
			bool PointInRange(Position position) => CoordInRange(position.X) && CoordInRange(position.Y);;
		}

		[Fact]
		public void EachPlanetHasADistinctPosition()
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = 800,
				PlanetCount = 100,
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(galaxy.Planets.Count, galaxy.Planets.Select(planet => planet.Position).Distinct().Count());
		}

		[Theory]
		[InlineData(800, 100, 1)]
		[InlineData(800, 100, 10)]
		[InlineData(800, 100, 25)]
		public void MinDistanceBetweenPlanetsIsValid(int size, int planets, int minDistance)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
				MinimumDistanceBetweenPlanets = minDistance
			};

			var galaxy = generator.Generate(settings);

			Assert.All(galaxy.Planets, planet => Assert.True(MinDistanceKept(planet)));

			bool PassesSocialDistancingRule(Planet p1, Planet p2) => p1 != p2 ? p1.Position.DistanceTo(p2.Position) >= minDistance : true;
			bool MinDistanceKept(Planet planet) => galaxy.Planets.All(otherPlanet => PassesSocialDistancingRule(planet, otherPlanet));
		}

		[Theory]
		[InlineData(100, 1000, 10)]
		[InlineData(100, 10, 100)]
		public void NotEnoughSpaceForPlanetsCausesTimeout(int size, int planets, int minDistance)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
				MinimumDistanceBetweenPlanets = minDistance,
			};

			Assert.Throws<TimeoutException>(() => generator.Generate(settings));
		}
	}
}
