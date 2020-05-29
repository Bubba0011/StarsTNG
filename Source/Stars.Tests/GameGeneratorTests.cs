using Stars.Core;
using Stars.Core.Setup;
using System.Linq;
using Xunit;

namespace Stars.Tests
{
	public class GameGeneratorTests
	{
		private readonly Game baselineGame = CreateBaselineGame();

		[Fact]
		public void EachPlayerHasOneHomeworld()
		{
			int GetSettlementCount(Player owner)
			{
				return baselineGame.Galaxy.Planets
					.Count(p => p.Settlement?.OwnerId == owner.Id);
			}

			Assert.All(baselineGame.Galaxy.Players, player => Assert.Equal(1, GetSettlementCount(player)));
		}

		[Fact]
		public void HomeworldHasPerfectEnvironment()
		{
			Planet GetHomeworld(Player owner)
			{
				return baselineGame.Galaxy.Planets
					.Single(p => p.Settlement?.OwnerId == owner.Id);
			}

			Assert.All(baselineGame.Galaxy.Players, player => Assert.Equal(player.Race.EnvironmentPreferences, GetHomeworld(player).Details.Environment));
		}

		private static Game CreateBaselineGame()
		{
			var settings = new GameGeneratorSettings
			{
				GameName = "Baseline",

				PlayerNames = { "One", "Two", "Three", },

				GalaxySettings = new GalaxyGeneratorSettings
				{
					GalaxySize = 500,
					PlanetCount = 300,
				},
			};

			var generator = new GameGenerator();
			return generator.Generate(settings);
		}
	}
}
