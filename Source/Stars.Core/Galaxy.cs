using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class Galaxy : IGalaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();

		public IList<Player> Players { get; set; } = new List<Player>();

		public int Size { get; set; }

		public GalaxyBounds Bounds => new GalaxyBounds(Size);

		IEnumerable<IPlanet> IGalaxy.Planets => Planets;

		IEnumerable<Player> IGalaxy.Players => Players;
	}
}
