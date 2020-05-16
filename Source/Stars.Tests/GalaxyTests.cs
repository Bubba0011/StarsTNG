using Stars.Core;
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

		[Fact]
		public void RetrievesTheClosestPlanet()
		{
			var galaxy = new Galaxy
			{
				Bounds = new GalaxyBounds(800)
			};

			galaxy.Planets = new Planet[]
			{
				new Planet { Position = new Position(400, 400)},
				new Planet { Position = new Position(300, 300)},
				new Planet { Position = new Position(200, 200)}
			}.ToList();

			var closest = galaxy.ClosestPlanet(new Position(360, 360));

			Assert.IsType<Planet>(closest);
			Assert.Equal(galaxy.Planets[0], closest);
		}
	}
}
