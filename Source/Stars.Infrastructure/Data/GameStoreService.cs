using Stars.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stars.Infrastructure.Data
{
	public struct GameInfo
	{
		public int GameId { get; set; }
		public string Name { get; set; }
		public int GalaxySize { get; set; }
		public int PlanetCount { get; set; }
		public int PlayerCount { get; set; }
	}

	public class GameStoreService
	{
		private readonly Dictionary<int, Game> store = new Dictionary<int, Game>();

		public IEnumerable<GameInfo> GetGames()
		{
			lock (store)
			{
				return store
					.OrderBy(e => e.Key)
					.Select(e => MakeInfo(e.Key, e.Value))
					.ToArray();
			}

			GameInfo MakeInfo(int gameId, Game game)
			{
				return new GameInfo()
				{
					GameId = gameId,
					Name = game.Name,
					GalaxySize = game.Galaxy.Bounds.Size,
					PlanetCount = game.Galaxy.Planets.Count,
					PlayerCount = game.Galaxy.Players.Count,
				};
			}
		}

		public async IAsyncEnumerable<GameInfo> GetGamesAsync()
		{
			var games = GetGames();

			foreach (var game in games)
			{
				await Task.CompletedTask;
				yield return game;
			}
		}

		public int AddGame(Game game)
		{
			lock (store)
			{
				int id = store.Count + 1;
				store[id] = game;
				return id;
			}
		}

		public Task<int> AddGameAsync(Game game)
		{
			int id = AddGame(game);
			return Task.FromResult(id);
		}

		public Game GetGame(int gameId)
		{
			lock (store)
			{
				return store.ContainsKey(gameId) ? store[gameId] : null;
			}
		}

		public Task<Game> GetGameAsync(int gameId)
		{
			var game = GetGame(gameId);
			return Task.FromResult(game);
		}
	}
}
