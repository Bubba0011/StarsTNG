using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Views
{
	public class PlayerGalaxyView : IGalaxy
	{
		private readonly Game game;
		private readonly int playerId;
		private readonly List<ScannerSite> scanners;

		private Galaxy Galaxy => game.Galaxy;

		public GalaxyBounds Bounds => Galaxy.Bounds;
		public IEnumerable<IPlanet> Planets => Galaxy.Planets.Select(Project);
		public IEnumerable<IFleet> Fleets => GetFleets();

		public PlayerGalaxyView(Game game, int playerId)
		{
			this.game = game;
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

			var turn = game.History.GetPlayerView(playerId, planet.Id);
			if (turn.HasValue)
			{
				var history = game.History.GetPlanet(turn.Value, planet.Id);
				return new OldScannedPlanet(planet, history!);
			}
			else
			{
				return new UnknowPlanet(planet);
			}
		}

		private IEnumerable<IFleet> GetFleets()
		{
			foreach (var fleet in Galaxy.Fleets)
			{
				if (fleet.OwnerId == playerId)
				{
					yield return new OwnedFleet(fleet);
				}
				else if (InScannerRange(fleet.Position))
				{
					yield return fleet.GetDefaultView(); // TODO
				}
			}
		}

		private bool InScannerRange(Position position)
		{
			return scanners.Any(scanner => scanner.InRange(position));
		}

		private IEnumerable<ScannerSite> GetScanners()
		{
			foreach (var planet in Galaxy.Planets)
			{
				if (planet.Settlement?.OwnerId == playerId)
				{
					yield return new ScannerSite(planet.Position, planet.Settlement.ScannerRange);
				}
			}

			foreach (var fleet in Galaxy.Fleets)
			{
				if (fleet.OwnerId == playerId)
				{
					yield return new ScannerSite(fleet.Position, fleet.ScannerRange);
				}
			}
		}

		internal IEnumerable<Planet> GetScannedPlanets()
		{
			bool CanScan(Planet planet)
			{
				return planet.Settlement?.OwnerId == playerId || InScannerRange(planet.Position);
			}

			return Galaxy.Planets.Where(CanScan);
		}
	}
}
