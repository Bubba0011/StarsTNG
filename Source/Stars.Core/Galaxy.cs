using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class Galaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();
		public IList<Player> Players { get; set; } = new List<Player>();
		public IList<Fleet> Fleets { get; set; } = new List<Fleet>();
		public GalaxyBounds Bounds { get; set; }

		public IGalaxy GetDefaultView() => new DefaultGalaxyView(this);

		public Fleet AddFleet(Fleet fleet)
		{
			fleet.Id = 0;
			Fleets.Add(fleet);
			fleet.Id = Fleets.Max(f => f.Id + 1);
			return fleet;
		}
	}
}
