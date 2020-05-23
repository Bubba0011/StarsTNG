using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public interface IGalaxy
	{
		IEnumerable<IPlanet> Planets { get; }
		IEnumerable<IPlayer> Players { get; }
		GalaxyBounds Bounds { get; }
	}

	public static class IGalaxyExtensions
	{
		public static IPlanet ClosestPlanet(this IGalaxy galaxy, Position target)
		{
			return galaxy.Planets
				.OrderBy(planet => target.DistanceTo(planet.Position))
				.First();
		}
	}
}
