using System.Collections.Generic;

namespace Stars.Core
{
	public class Galaxy : IGalaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();

		public IList<Player> Players { get; set; } = new List<Player>();

		public GalaxyBounds Bounds { get; set; }

		IEnumerable<IPlanet> IGalaxy.Planets => Planets;

		IEnumerable<Player> IGalaxy.Players => Players;
	}
}
