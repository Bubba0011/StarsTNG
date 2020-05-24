using System;

namespace Stars.Core
{
	public class Game
	{
		public string? Name { get; set; }
		public GameSettings Settings { get; set; }
		public Galaxy Galaxy { get; set; }
		public int Turn { get; set; } = 1;

		public Game()
		{
			Settings = new GameSettings();
			Galaxy = new Galaxy();
		}

		public Game(GameSettings settings, Galaxy galaxy)
		{
			Settings = settings;
			Galaxy = galaxy;
		}

		public void Update()
		{
			Turn += Settings.TimeStep;

			foreach (var planet in Galaxy.Planets)
			{
				if (planet.Settlement != null)
				{
					UpdateSettlement(planet.Settlement);
				}
			}

			foreach (var fleet in Galaxy.Fleets)
			{
				UpdateFleet(fleet);
			}

			static void UpdateSettlement(Settlement settlement)
			{
				// TODO: Player.Race.GrowthRate...
				double rate = 0.07;
				double rawDelta = settlement.Population * rate;
				int cleanDelta = 100 * (int)Math.Round(rawDelta / 100, 0);
				settlement.Population += cleanDelta;
			}

			static void UpdateFleet(Fleet fleet)
			{
				const double Warp7 = 49;
				fleet.Move(Warp7);
			}
		}
	}

	public class GameSettings
	{
		public int TimeStep => 1;
	}
}
