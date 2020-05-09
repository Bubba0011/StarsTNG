using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public interface IGalaxy
	{
		IEnumerable<Planet> Planets { get; }
		IEnumerable<Player> Players { get; }
		GalaxyBounds Bounds { get; }
	}

	public static class IGalaxyExtensions
	{
		public static Planet ClosestPlanet(this IGalaxy galaxy, Position target)
		{
			return galaxy.Planets
				.OrderBy(planet => target.DistanceTo(planet.Position))
				.First();
		}
	}
}
