using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Views
{
	public class PlayerGalaxyView : IGalaxy
	{
		private readonly Game game;
		private readonly int playerId;
		private readonly List<ScannerSite> scanners;

		private Galaxy galaxy => game.Galaxy;

		public GalaxyBounds Bounds => galaxy.Bounds;
		public IEnumerable<IPlanet> Planets => galaxy.Planets.Select(Project);
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
			else
			{
				return new UnknowPlanet(planet);
			}
		}

		private IEnumerable<IFleet> GetFleets()
		{
			foreach (var fleet in galaxy.Fleets)
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
			foreach (var planet in galaxy.Planets)
			{
				if (planet.Settlement?.OwnerId == playerId)
				{
					yield return new ScannerSite(planet.Position, planet.Settlement.ScannerRange);
				}
			}

			foreach (var fleet in galaxy.Fleets)
			{
				if (fleet.OwnerId == playerId)
				{
					yield return new ScannerSite(fleet.Position, fleet.ScannerRange);
				}
			}
		}
	}

	public class PlayerPlayerView : IPlayer
	{
		private Game game;
		private readonly Player player;

		public int Id => player.Id;
		public string? Name => player.Name;

		public PlayerPlayerView(Player player, Game game)
		{
			this.game = game;
			this.player = player;
		}

		public double? GetPlanetValue(IPlanet planet)
		{
			if (planet.Details != null)
			{
				return game.Rules.CalculatePlanetValue(planet.Details, player.Race);
			}
			else
			{
				return null;
			}
		}
	}
}
