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
				PlanetCount = 0,
			};

			var galaxy = generator.Generate(settings);

			Assert.Equal(size, galaxy.Bounds.Size);
		}

		[Theory]
		[InlineData(100, 25, 10)]
		[InlineData(800, 125, 0)]
		[InlineData(2000, 500, 20)]
		public void PlanetsHaveValidPositions(int size, int planets, int padding)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
				Padding = padding,
			};

			var galaxy = generator.Generate(settings);
			var bounds = new GalaxyBounds(size);
			var min = bounds.Min + padding;
			var max = bounds.Max - padding;

			Assert.All(galaxy.Planets, planet => Assert.True(PointInRange(planet.Position)));

			bool CoordInRange(int coord) => coord >= min && coord <= max;
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
		[InlineData(100, 99, 10)]
		public void NotEnoughSpaceForPlanetsCausesException(int size, int planets, int minDistance)
		{
			var generator = new GalaxyGenerator();

			var settings = new GalaxyGeneratorSettings()
			{
				GalaxySize = size,
				PlanetCount = planets,
				MinimumDistanceBetweenPlanets = minDistance,
			};

			Assert.Throws<OutOfSpaceException>(() => generator.Generate(settings));
		}
	}
}
