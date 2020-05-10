using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class PlayerView : IGalaxy
	{
		private readonly Galaxy galaxy;
		private readonly int playerId;

		public GalaxyBounds Bounds => galaxy.Bounds;
		public IEnumerable<IPlanet> Planets => galaxy.Planets.Select(Project);
		public IEnumerable<Player> Players => galaxy.Players;

		private readonly List<Planet> colonies;

		public PlayerView(Galaxy galaxy, int playerId)
		{
			this.galaxy = galaxy;
			this.playerId = playerId;

			colonies = galaxy.Planets
				.Where(p => p.Settlement?.OwnerId == playerId)
				.ToList();
		}

		private IPlanet Project(Planet planet)
		{
			if (planet.Settlement?.OwnerId == playerId)
			{
				return new ScannedPlanet(planet);
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
			//const double ScannerRange = 100;

			return colonies.Any(c => c.Position.DistanceTo(position) <= c.Settlement.ScannerRange);
		}
	}

	class ScannedPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => planet.Details;
		public ISettlement? Settlement => planet.Settlement;

		public ScannedPlanet(Planet planet)
		{
			this.planet = planet;
		}
	}

	class UnknowPlanet : IPlanet
	{
		private readonly Planet planet;

		public int Id => planet.Id;
		public Position Position => planet.Position;
		public string? Name => planet.Name;
		public PlanetDetails? Details => null;
		public ISettlement? Settlement => null; 

		public UnknowPlanet(Planet planet)
		{
			this.planet = planet;
		}
	}
}
