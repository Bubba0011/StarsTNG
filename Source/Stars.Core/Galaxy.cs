using System.Collections.Generic;

namespace Stars.Core
{
	public class Galaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();
		public IList<Player> Players { get; set; } = new List<Player>();
		public IList<Fleet> Fleets { get; set; } = new List<Fleet>();
		public GalaxyBounds Bounds { get; set; }

		public IGalaxy GetDefaultView() => new DefaultGalaxyView(this);
	}
}
