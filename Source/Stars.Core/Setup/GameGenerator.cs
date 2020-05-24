﻿using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Setup
{
	public class GameGenerator
	{
		private readonly IRandom rnd;

		public GameGenerator(IRandom? random = null)
		{
			rnd = random ?? new DefaultRandom();
		}

		public Game Generate(GameGeneratorSettings settings)
		{
			// Generate galaxy
			var generator = new GalaxyGenerator(rnd);
			var galaxy = generator.Generate(settings.GalaxySettings);

			// Populate galaxy
			var players = settings.PlayerNames
				.Select((name, index) => new Player()
				{
					Id = index + 1,
					Name = name,
				});

			foreach (var player in players)
			{
				galaxy.Players.Add(player);

				var uninhabitedPlanets = galaxy.Planets
					.Where(p => p.Settlement == null)
					.ToArray();

				var homeworld = rnd.PickOne(uninhabitedPlanets);

				homeworld.Settlement = new Settlement()
				{
					OwnerId = player.Id,
					Population = 10_000,
					ScannerRange = 100,
				};

				var fleet = new Fleet()
				{
					Id = galaxy.Fleets.Count + 1,
					OwnerId = player.Id,
					Position = homeworld.Position,
					Name = $"{player.Name} Fleet #1",
					ScannerRange = 50,
					Waypoints = new List<Position> { Position.Zero, },
				};
				galaxy.Fleets.Add(fleet);
			}

			var gameSettings = new GameSettings();

			return new Game(gameSettings, galaxy)
			{
				Name = settings.GameName,
			};
		}
	}
}
