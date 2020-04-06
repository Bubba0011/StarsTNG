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
			Assert.All(galaxy.Planets, planet => Assert.True(MinDistanceKept(planet.Position)));

			bool InRange(int n) => n >= 0 && n < size;
			bool PointInRange(Position p) => InRange(p.X) && InRange(p.Y);

			bool MinDistanceKept(Position p)
			{
				bool PassesSocialDistancingRule(Position p1, Position p2)
				{
					if (p1.Equals(p2))
					{
						return true;
					}
					var dx = p1.X - p2.X;
					var dy = p1.Y - p2.Y;
					return Math.Sqrt(dx * dx + dy * dy) > minDistance;
				}

				return galaxy.Planets.All(planet => PassesSocialDistancingRule(planet.Position, p));
			}
		}
	}
}
