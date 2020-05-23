using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public interface IGalaxy
	{
		IEnumerable<IPlanet> Planets { get; }
		IEnumerable<IPlayer> Players { get; }
		IEnumerable<IFleet> Fleets { get; }
		GalaxyBounds Bounds { get; }
	}

	public static class IGalaxyExtensions
	{
		public static IEnumerable<ISpaceObject> GetSpaceObjects(this IGalaxy galaxy)
		{
			return galaxy.Planets
				.Cast<ISpaceObject>()
				.Concat(galaxy.Fleets);
		}

		public static IPlanet ClosestPlanet(this IGalaxy galaxy, Position target)
		{
			return galaxy.Planets
				.OrderBy(planet => target.DistanceTo(planet.Position))
				.FirstOrDefault();
		}

		public static IFleet ClosestFleet(this IGalaxy galaxy, Position target)
		{
			return galaxy.Fleets
				.OrderBy(fleet => target.DistanceTo(fleet.Position))
				.FirstOrDefault();
		}

		public static ISpaceObject ClosestSpaceObject(this IGalaxy galaxy, Position target)
		{
			return galaxy.GetSpaceObjects()
				.OrderBy(obj => target.DistanceTo(obj.Position))
				.FirstOrDefault();
		}
	}
}
