using System;
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

	public class GameRules
	{
		public int TimeStep => 1;

		public double CalculatePlanetValue(Planet planet, Race race)
		{
			static double F(int actual, int preferred)
			{
				const int zone = 25;

				int delta = Math.Abs(actual - preferred);

				return delta < zone
					? Math.Pow((zone - delta) / zone, 2)
					: -10;
			}

			var actual = planet.Details.Environment;
			var preferred = race.EnvironmentPreferences;

			var g = F(actual.Gravity, preferred.Gravity);
			var r = F(actual.Radiation, preferred.Radiation);
			var t = F(actual.Temperature, preferred.Temperature);

			var grt = g + r + t;

			return grt > 0 
				? Math.Sqrt(grt) / Math.Sqrt(3) 
				: 0;
		}

		public int CalculatePopulationGrowth(Planet planet, Player owner)
		{
			var settlement = planet.Settlement;
			if (settlement == null)
			{
				return 0;
			}

			int basePopulationCapacity = owner.Race.PlanetPopulationCapacity;
			double baseGrowthRate = owner.Race.PopulationGrowthRate;
			double planetHabFactor = CalculatePlanetValue(planet, owner.Race);

			double effectivePopulationCapacity = basePopulationCapacity * planetHabFactor;
			double capacityPct = settlement.Population / effectivePopulationCapacity;

			double crowdingFactor = capacityPct <= 0.25 ? 1 : 16.0 / 9 * Math.Pow(1 - capacityPct, 2);
			double effectiveGrowthRate = baseGrowthRate * planetHabFactor * crowdingFactor;

			return (int)Math.Round(settlement.Population * effectiveGrowthRate);
		}
	}
}
