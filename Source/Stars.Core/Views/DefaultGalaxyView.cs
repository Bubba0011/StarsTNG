using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	class DefaultGalaxyView : IGalaxy
	{
		private readonly Galaxy galaxy;

		public IEnumerable<IPlanet> Planets => galaxy.Planets.Select(p => p.GetDefaultView());
		public IEnumerable<IPlayer> Players => galaxy.Players.Select(p => p.GetDefaultView());
		public GalaxyBounds Bounds => galaxy.Bounds;

		public DefaultGalaxyView(Galaxy galaxy)
		{
			this.galaxy = galaxy;
		}
	}
}
