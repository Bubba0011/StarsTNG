using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Views
{
	public class PlayerGalaxyView : IGalaxy
	{
		private readonly Galaxy galaxy;
		private readonly int playerId;
		private readonly List<ScannerSite> scanners;

		public GalaxyBounds Bounds => galaxy.Bounds;
		public IEnumerable<IPlanet> Planets => galaxy.Planets.Select(Project);
		public IEnumerable<IPlayer> Players => galaxy.Players;

		public PlayerGalaxyView(Galaxy galaxy, int playerId)
		{
			this.galaxy = galaxy;
			this.playerId = playerId;

			scanners = GetScanners().ToList();
		}

		private IPlanet Project(Planet planet)
		{
			if (planet.Settlement?.OwnerId == playerId)
			{
				return new OwnedPlanet(planet);
			}
			else if (InScannerRange(planet.Position))
			{
				return new ScannedPlanet(planet);
			}
			else
			{
				return new UnknowPlanet(planet);
			}
		}

		private bool InScannerRange(Position position)
		{
			return scanners.Any(scanner => scanner.InRange(position));
		}

		private IEnumerable<ScannerSite> GetScanners()
		{
			foreach (var planet in galaxy.Planets)
			{
				if (planet.Settlement?.OwnerId == playerId)
				{
					yield return new ScannerSite(planet.Position, planet.Settlement.ScannerRange);
				}
			}
		}
	}
}
