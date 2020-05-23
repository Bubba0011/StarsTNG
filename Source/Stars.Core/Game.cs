﻿using System;

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

			static void UpdateSettlement(Settlement settlement)
			{
				// TODO: Player.Race.GrowthRate...
				double rate = 0.07;
				double rawDelta = settlement.Population * rate;
				int cleanDelta = 100 * (int)Math.Round(rawDelta / 100, 0);
				settlement.Population += cleanDelta;

				settlement.ScannerRange = Math.Min(500, (int)Math.Sqrt(5 * settlement.Population));
			}
		}
	}

	public class GameSettings
	{
		public int TimeStep => 1;
	}
}
