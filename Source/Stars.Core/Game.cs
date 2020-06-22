using System;
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
		public EntityStore<Player> Players { get; set; } = new EntityStore<Player>();
		public HistoryStore History { get; set; } = new HistoryStore();

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
			// Save history
			History.Store(this);

			Turn += Rules.TimeStep;

			UpdatePlanets();
			UpdateFleets();
			ExecuteColonizeOrders();
			ExecuteAssaultOrders();

			UpdateScoreboard();
		}

		private void UpdatePlanets()
		{
			foreach (var planet in Galaxy.Planets)
			{
				if (planet.Settlement != null)
				{
					BuildStuff(planet);
					UpdatePopulation(planet);
				}
			}

			void BuildStuff(Planet populatedPlanet)
			{
				Settlement settlement = populatedPlanet.Settlement!;

				int resources = settlement.Population.Civilians / 1000;
				var output = settlement.BuildQueue.Build(resources);
				resources -= output.ConsumedResources;

				foreach (var item in output.CompletedItems)
				{
					if (item.ItemToBuild.Equals(BuildMenuItem.ScoutShip))
					{
						LaunchShip(50, "Scout");
					}
					else if (item.ItemToBuild.Equals(BuildMenuItem.ColonyShip))
					{
						int settlers = Math.Min(5000, settlement.Population.Civilians / 2);
						var passengers = new Population(settlers);
						settlement.Population -= passengers;

						var ship = LaunchShip(20, "Mayflower");
						ship.Passengers = passengers;
					}
					else if (item.ItemToBuild.Equals(BuildMenuItem.AssaultShip))
					{
						int recruites = Math.Min(10_000, settlement.Population.Civilians / 5);
						int marines = recruites / 2;
						settlement.Population -= new Population(recruites);

						var ship = LaunchShip(20, "MEU");
						ship.Passengers = new Population(0, marines);
					}
					else if (item.ItemToBuild.Equals(BuildMenuItem.Garrison))
					{
						int recruites = Math.Min(10_000, settlement.Population.Civilians / 5);
						int marines = recruites / 2;
						settlement.Population += new Population(-recruites, marines);
					}
				}

				Fleet LaunchShip(int scanner, string name)
				{
					var ship = new Fleet
					{
						OwnerId = settlement.OwnerId,
						Position = populatedPlanet.Position,
						ScannerRange = scanner,
					};

					Galaxy.Fleets.Add(ship);
					ship.Name = $"{name} #{ship.Id}";

					return ship;
				}
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
		}

		private void UpdateFleets()
		{
			foreach (var fleet in Galaxy.Fleets)
			{
				const double Warp7 = 49;
				fleet.Move(Warp7);
			}
		}

		private void ExecuteColonizeOrders()
		{
			var colonizers = Galaxy.Fleets
				.Where(f => f.Passengers.Civilians > 0)
				.Where(f => f.Waypoints?.Any() != true)
				.ToArray();

			foreach (var fleet in colonizers)
			{
				var planet = Galaxy.Planets.SingleOrDefault(p => p.Position == fleet.Position);

				if (planet != null && CanColonize(planet, fleet))
				{
					ColonizePlanet(planet, fleet);
				}
			}

			bool CanColonize(Planet planet, Fleet colonyFleet)
			{
				if (planet.Settlement == null)
				{
					var race = GetPlayer(colonyFleet.OwnerId)!.Race;
					var value = Rules.CalculatePlanetValue(planet.Details, race);

					return value > 0;
				}

				return false;
			}

			void ColonizePlanet(Planet planet, Fleet colonyFleet)
			{
				Galaxy.Fleets.Remove(colonyFleet);

				planet.Settlement = new Settlement
				{
					OwnerId = colonyFleet.OwnerId,
					Population = colonyFleet.Passengers,
					ScannerRange = 100,
				};
			}
		}

		private void ExecuteAssaultOrders()
		{
			var assaultGroups = Galaxy.Fleets
				.Where(f => f.Passengers.Marines > 0)
				.Where(f => f.Waypoints?.Any() != true)
				.GroupBy(f => f.Position)
				.ToArray();

			foreach (var group in assaultGroups)
			{
				DeployMarines(group);
			}

			void DeployMarines(IEnumerable<Fleet> fleets)
			{
				var location = fleets.First().Position;
				var planet = Galaxy.Planets.SingleOrDefault(p => p.Position == location);
				if (planet?.Settlement == null) return;

				var assaultGroups = fleets
					.Where(f => f.OwnerId != planet.Settlement.OwnerId)
					.GroupBy(f => f.OwnerId);

				// 1) Unload reinforcements if attack is imminent
				if (assaultGroups.Any())
				{
					var reinforcements = fleets.Where(f => f.OwnerId == planet.Settlement.OwnerId);

					foreach (var def in reinforcements)
					{
						planet.Settlement.Population += def.Passengers;
						Galaxy.Fleets.Remove(def);
					}
				}

				// 2) Attackers attack
				foreach (var group in assaultGroups)
				{
					int attackerId = group.Key;
					var assaultForce = new Population();
					foreach (var att in group)
					{
						assaultForce += att.Passengers;
						Galaxy.Fleets.Remove(att);
					}

					ResolveGroundCombat(planet.Settlement, assaultForce, attackerId);
				}
			}

			void ResolveGroundCombat(Settlement settlement, Population assaultForce, int attackerId)
			{
				var defenseForce = settlement.Population;
				defenseForce.Civilians /= 10;

				var defenseValue = ForceValue(defenseForce);
				var attackValue = ForceValue(assaultForce);

				if (attackValue > defenseValue)
				{
					var killRate = 0.75 * defenseValue / attackValue;
					assaultForce -= CalculateCasualties(assaultForce, killRate);

					settlement.Population -= defenseForce;
					settlement.Population += assaultForce;

					settlement.OwnerId = attackerId;

					// TODO: Add history entry for defeated player?
				}
				else
				{
					var killRate = 0.75 * attackValue / defenseValue;
					settlement.Population -= CalculateCasualties(defenseForce, killRate);
				}

				// TODO: Report event to players involved...

				static double ForceValue(Population force) => 2 * force.Marines + force.Civilians;

				static Population CalculateCasualties(Population force, double killRate)
				{
					return new Population
					{
						Civilians = (int)(force.Civilians * killRate),
						Marines = (int)(force.Marines * killRate),
					};
				}
			}
		}

		private void UpdateScoreboard()
		{
			Scoreboard = Players
				.Select(CalculateScore)
				.OrderByDescending(item => item.Score)
				.ThenBy(item => item.PlayerId)
				.ToArray();
		}

		private PlayerScore CalculateScore(Player player)
		{
			var settlements = Galaxy.Planets
				.Where(p => p.Settlement?.OwnerId == player.Id)
				.Select(p => p.Settlement!);

			var population = settlements.Sum(s => s.Population.Civilians);
			var planets = settlements.Count();

			var score = population / 1000 + planets * 10;

			return new PlayerScore(player.Id, player.Name ?? $"Player #{player.Id}", score);
		}

		private Player? GetPlayer(int playerId)
		{
			return Players.SingleOrDefault(p => p.Id == playerId);
		}
	}
}
