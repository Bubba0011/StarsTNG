using System;
using System.Linq;

namespace Stars.Core
{
	public class Game
	{
		public GameSettings Settings { get; set; }
		public Galaxy Galaxy { get; set; }
		public int Turn { get; set; } = 1;

		public Game()
		{
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

			void UpdateSettlement(Settlement settlement)
			{
				// TODO: Player.Race.GrowthRate...
				double rate = 1.05;
				double rawDelta = settlement.Population * rate;
				int cleanDelta = 100 * (int)Math.Round(rawDelta / 100, 0);
				settlement.Population += cleanDelta;

				settlement.ScannerRange = (int)Math.Sqrt(settlement.Population / 100);
			}
		}
	}

	public class GameSettings
	{
		public int TimeStep => 1;
	}
}
