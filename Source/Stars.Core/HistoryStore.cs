using Stars.Core.Views;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class HistoryStore
	{
		public List<PlayerRecord> Players { get; set; } = new List<PlayerRecord>();
		public List<PlanetRecord> Planets { get; set; } = new List<PlanetRecord>();

		public void Store(Game game)
		{
			foreach (var player in game.Players)
			{
				var view = new PlayerGalaxyView(game, player.Id);
				var pids = view.GetScannedPlanets().Select(planet => planet.Id);
				StorePlayerView(game.Turn, player.Id, pids);
			}

			foreach (var planet in game.Galaxy.Planets)
			{
				StorePlanet(game.Turn, planet);
			}

			foreach (var fleet in game.Galaxy.Fleets)
			{

			}
		}

		private void StorePlayerView(int turn, int playerId, IEnumerable<int> planetIds)
		{
			var record = new PlayerRecord
			{
				Turn = turn,
				PlayerId = playerId,
				PlanetIds = planetIds.ToHashSet(),
			};

			Players.Add(record);
		}

		private void StorePlanet(int turn, Planet planet)
		{
			var record = new PlanetRecord
			{
				Turn = turn,
				PlanetId = planet.Id,
				OwnerId = planet.Settlement?.OwnerId,
				Population = planet.Settlement?.Population,
				ScannerRange = planet.Settlement?.ScannerRange,
			};

			Planets.Add(record);
		}

		public int? GetPlayerView(int playerId, int planetId)
		{
			var hit = Players
				.Where(rec => rec.PlayerId == playerId)
				.OrderByDescending(rec => rec.Turn)
				.FirstOrDefault(rec => rec.PlanetIds.Contains(planetId));

			return hit?.Turn;
		}

		public PlanetRecord? GetPlanet(int turn, int planetId)
		{
			return Planets
				.Where(p => p.Turn == turn)
				.Where(p => p.PlanetId == planetId)
				.SingleOrDefault();
		}
	}

	public class PlayerRecord
	{
		public int Turn { get; set; }
		public int PlayerId { get; set; }
		public HashSet<int> PlanetIds { get; set; } = new HashSet<int>();
	}

	public class PlanetRecord
	{
		public int Turn { get; set; }
		public int PlanetId { get; set; }
		public int? OwnerId { get; set; }
		public int? Population { get; set; }
		public int? ScannerRange { get; set; }
	}
}
