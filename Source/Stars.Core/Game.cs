using System.Linq;

namespace Stars.Core
{
	public class Game
	{
		public string? Name { get; set; }
		public GameRules Rules { get; set; }
		public Galaxy Galaxy { get; set; }
		public int Turn { get; set; } = 1;

		public Game()
		{
			Rules = new GameRules();
			Galaxy = new Galaxy();
		}

		public Game(GameRules settings, Galaxy galaxy)
		{
			Rules = settings;
			Galaxy = galaxy;
		}

		public void Update()
		{
			Turn += Rules.TimeStep;

			foreach (var planet in Galaxy.Planets)
			{
				if (planet.Settlement != null)
				{
					UpdatePopulation(planet);
				}
			}

			foreach (var fleet in Galaxy.Fleets)
			{
				UpdateFleet(fleet);
			}

			void UpdatePopulation(Planet populatedPlanet)
			{
				Settlement settlement = populatedPlanet.Settlement!;
				Player? owner = GetPlayer(settlement.OwnerId);

				if (owner != null)
				{
					settlement.Population += Rules.CalculatePopulationGrowth(populatedPlanet, owner);
				}
			}

			static void UpdateFleet(Fleet fleet)
			{
				const double Warp7 = 49;
				fleet.Move(Warp7);
			}
		}

		private Player? GetPlayer(int playerId)
		{
			return Galaxy.Players.SingleOrDefault(p => p.Id == playerId);
		}
	}
}
