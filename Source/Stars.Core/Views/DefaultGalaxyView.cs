using System.Collections.Generic;

namespace Stars.Core
{
	class DefaultGalaxyView : IGalaxy
	{
		private readonly Galaxy galaxy;

		public IEnumerable<IPlanet> Planets => galaxy.Planets;
		public IEnumerable<IPlayer> Players => galaxy.Players;
		public GalaxyBounds Bounds => galaxy.Bounds;

		public DefaultGalaxyView(Galaxy galaxy)
		{
			this.galaxy = galaxy;
		}
	}
}
