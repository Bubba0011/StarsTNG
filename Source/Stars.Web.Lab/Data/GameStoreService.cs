using Stars.Core;
using Stars.Core.Setup;
using System.Threading.Tasks;

namespace Stars.Web.Lab.Data
{
	public class GameStoreService
	{
		private readonly GalaxyGeneratorSettings DefaultSettings = new GalaxyGeneratorSettings()
		{
			PlanetCount = 333,
		};

		private readonly object theLock = new object();
		private Game theGame;

		public Game GetGame()
		{
			return theGame ?? Regenerate(DefaultSettings);
		}

		public async Task<Game> GetGameAsync()
		{
			return theGame ?? await RegenerateAsync(DefaultSettings);
		}

		public Game Regenerate(GalaxyGeneratorSettings settings)
		{
			var newGame = GenerateGame(settings);
			return SetGame(newGame);
		}

		public async Task<Game> RegenerateAsync(GalaxyGeneratorSettings settings)
		{
			var newGame = await Task.Run(() => GenerateGame(settings));
			return SetGame(newGame);
		}
		
		private Game SetGame(Game game)
		{
			lock (theLock)
			{
				theGame = game;
			}

			return game;
		}

		private Game GenerateGame(GalaxyGeneratorSettings settings)
		{
			// Configure
			var gameGenSettings = new GameGeneratorSettings()
			{
				GalaxySettings = settings,
				PlayerNames = { "Federation", "The Borg", "Romulans" },
			};

			// Generate
			var generator = new GameGenerator();
			return generator.Generate(gameGenSettings);
		}
	}
}
