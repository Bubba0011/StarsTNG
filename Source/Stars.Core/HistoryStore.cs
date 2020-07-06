using Stars.Core.Views;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class HistoryStore
	{
		public List<PlayerRecord> Players { get; set; } = new List<PlayerRecord>();
		public List<PlanetRecord> Planets { get; set; } = new List<PlanetRecord>();
		public List<FleetRecord> Fleets { get; set; } = new List<FleetRecord>();

		public void Store(Game game)
		{
			foreach (var player in game.Players)
			{
				var view = new PlayerGalaxyView(game, player.Id);
				var pids = view.GetScannedPlanets().Select(planet => planet.Id);
				var fids = view.GetScannedFleets().Select(fleet => fleet.Id);
				StorePlayerView(game.Time, player.Id, pids, fids);
			}

			foreach (var planet in game.Galaxy.Planets)
			{
				StorePlanet(game.Time, planet);
			}

			foreach (var fleet in game.Galaxy.Fleets)
			{
				StoreFleet(game.Time, fleet);
			}
		}

		private void StorePlayerView(SpaceTime time, int playerId, IEnumerable<int> planetIds, IEnumerable<int> fleetIds)
		{
			var record = new PlayerRecord
			{
				Time = time,
				PlayerId = playerId,
				PlanetIds = planetIds.ToHashSet(),
				FleetIds = fleetIds.ToHashSet(),
			};

			Players.Add(record);
		}

		private void StorePlanet(SpaceTime time, Planet planet)
		{
			var record = new PlanetRecord
			{
				Time = time,
				PlanetId = planet.Id,
				OwnerId = planet.Settlement?.OwnerId,
				Population = planet.Settlement?.Population,
				ScannerRange = planet.Settlement?.Installations.Scanner,
			};

			Planets.Add(record);
		}

		private void StoreFleet(SpaceTime time, Fleet fleet)
		{
			var record = new FleetRecord
			{
				Time = time,
				FleetId = fleet.Id,
				Position = fleet.Position,
			};

			Fleets.Add(record);
		}

		public SpaceTime? GetPlayerView(int playerId, int planetId)
		{
			var hit = Players
				.Where(rec => rec.PlayerId == playerId)
				.OrderByDescending(rec => rec.Time)
				.FirstOrDefault(rec => rec.PlanetIds.Contains(planetId));

			return hit?.Time;
		}

		public PlanetRecord? GetPlanet(SpaceTime time, int planetId)
		{
			return Planets
				.Where(p => p.Time == time)
				.Where(p => p.PlanetId == planetId)
				.SingleOrDefault();
		}

		public IEnumerable<FleetRecord> GetFleet(int fleetId)
		{
			return Fleets.Where(f => f.FleetId == fleetId);
		}

		public IEnumerable<FleetRecord> GetFleet(int fleetId, int viewingPlayerId)
		{
			var turns = Players
				.Where(p => p.PlayerId == viewingPlayerId)
				.Where(p => p.FleetIds.Contains(fleetId))
				.Select(p => p.Time)
				.ToHashSet();

			return Fleets
				.Where(f => f.FleetId == fleetId)
				.Where(f => turns.Contains(f.Time));
		}
	}

	public class PlayerRecord
	{
		public SpaceTime Time { get; set; }
		public int PlayerId { get; set; }
		public HashSet<int> PlanetIds { get; set; } = new HashSet<int>();
		public HashSet<int> FleetIds { get; set; } = new HashSet<int>();
	}

	public class PlanetRecord
	{
		public SpaceTime Time { get; set; }
		public int PlanetId { get; set; }
		public int? OwnerId { get; set; }
		public Population? Population { get; set; }
		public int? ScannerRange { get; set; }
	}

	public class FleetRecord
	{
		public SpaceTime Time { get; set; }
		public int FleetId { get; set; }
		public Position Position { get; set; }
	}
}
