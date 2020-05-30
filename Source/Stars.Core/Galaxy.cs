using System.Collections.Generic;

namespace Stars.Core
{
	public class Galaxy
	{
		public EntityStore<Planet> Planets { get; set; } = new EntityStore<Planet>();
		public EntityStore<Fleet> Fleets { get; set; } = new EntityStore<Fleet>();
		public GalaxyBounds Bounds { get; set; }

		public IGalaxy GetDefaultView() => new DefaultGalaxyView(this);
	}
}
