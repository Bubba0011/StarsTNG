﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class Game
	{
		public string? Name { get; set; }
		public GameRules Rules { get; set; }
		public Galaxy Galaxy { get; set; }
		public int Turn { get; set; } = 1;
		public IList<PlayerScore> Scoreboard { get; set; } = new PlayerScore[0];
		public IList<Player> Players { get; set; } = new List<Player>();

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

			// Planets build stuff and population grows
			foreach (var planet in Galaxy.Planets)
			{
				if (planet.Settlement != null)
				{
					BuildStuff(planet);
					UpdatePopulation(planet);
				}
			}

			// Fleets move
			foreach (var fleet in Galaxy.Fleets)
			{
				UpdateFleet(fleet);
			}

			// Colony ships colonize
			var colonizers = Galaxy.Fleets
				.Where(f => f.ColonistCount > 0)
				.Where(f => f.Waypoints?.Any() != true)
				.ToArray();

			foreach (var fleet in colonizers)
			{
				Colonize(fleet);
			}

			// Update scoreboard...
			Scoreboard = Players
				.Select(CalculateScore)
				.OrderByDescending(item => item.Score)
				.ThenBy(item => item.PlayerId)
				.ToArray();

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

			void BuildStuff(Planet populatedPlanet)
			{
				Settlement settlement = populatedPlanet.Settlement!;

				int resources = settlement.Population / 1000;
				var output = settlement.BuildQueue.Build(resources).ToArray();

				foreach (var item in output)
				{
					if (item.ItemToBuild == BuildQueueItem.ScoutShip)
					{
						var scout = new Fleet
						{
							OwnerId = settlement.OwnerId,
							Position = populatedPlanet.Position,
							ScannerRange = 50,
						};

						Galaxy.AddFleet(scout);
						scout.Name = $"Scout #{scout.Id}";
					}
					else if (item.ItemToBuild == BuildQueueItem.ColonyShip)
					{
						int settlers = Math.Min(5000, settlement.Population / 2);
						settlement.Population -= settlers;

						var colonyShip = new Fleet
						{
							OwnerId = settlement.OwnerId,
							Position = populatedPlanet.Position,
							ScannerRange = 20,
							ColonistCount = settlers,
						};

						Galaxy.AddFleet(colonyShip);
						colonyShip.Name = $"Mayflower #{colonyShip.Id}";
					}
				}
			}

			void Colonize(Fleet colonyFleet)
			{
				var planet = Galaxy.Planets.SingleOrDefault(p => p.Position == colonyFleet.Position);

				if (planet != null && planet.Settlement == null)
				{
					var race = GetPlayer(colonyFleet.OwnerId)!.Race;
					var value = Rules.CalculatePlanetValue(planet.Details, race);

					if (value > 0)
					{
						Galaxy.Fleets.Remove(colonyFleet);

						planet.Settlement = new Settlement
						{
							OwnerId = colonyFleet.OwnerId,
							Population = colonyFleet.ColonistCount,
							ScannerRange = 100,
						};
					}
				}
			}
		}

		private PlayerScore CalculateScore(Player player)
		{
			var score = Galaxy.Planets
				.Where(p => p.Settlement?.OwnerId == player.Id)
				.Select(p => p.Settlement!)
				.Sum(s => s.Population);

			return new PlayerScore(player.Id, player.Name ?? $"Player #{player.Id}", score);
		}

		private Player? GetPlayer(int playerId)
		{
			return Players.SingleOrDefault(p => p.Id == playerId);
		}
	}

	public struct PlayerScore
	{
		public int PlayerId { get; }
		public string PlayerName { get; }
		public long Score { get; }

		public PlayerScore(int playerId, string playerName, long score)
		{
			PlayerId = playerId;
			PlayerName = playerName;
			Score = score;
		}
	}
}
