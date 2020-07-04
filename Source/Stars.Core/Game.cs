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
					item.ItemToBuild.Complete(Galaxy, populatedPlanet, settlement, msg => Notify(settlement.OwnerId, msg));
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
					Installations = new Installations
					{
						Scanner = 100,
					},
				};

				Notify(colonyFleet.OwnerId, $"Settlement established on planet #{planet.Id}", Mood.Good);
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
				var location = group.Key;
				var planet = Galaxy.Planets.SingleOrDefault(p => p.Position == location);
				if (planet?.Settlement != null)
				{
					DeployMarines(planet, group);
				}
			}

			void DeployMarines(Planet planet, IEnumerable<Fleet> fleets)
			{
				if (planet.Settlement == null) return;

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

					ResolveGroundCombat(planet, assaultForce, attackerId);
				}
			}

			void ResolveGroundCombat(Planet planet, Population assaultForce, int attackerId)
			{
				Settlement settlement = planet.Settlement!;
				Player defender = GetPlayer(settlement.OwnerId)!;
				Player attacker = GetPlayer(attackerId)!;

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

					settlement.OwnerId = attacker.Id;

					var defenderMsg = $"Our settlement on Planet #{planet.Id} was overrun by {attacker.Name} forces.";
					Notify(defender.Id, defenderMsg, Mood.Bad);

					var attackerMsg = $"We have conquered the {defender.Name} settlement on Planet #{planet.Id}";
					Notify(attacker.Id, attackerMsg, Mood.Good);

					// TODO: Add history entry for defeated player?
				}
				else
				{
					var killRate = 0.75 * attackValue / defenseValue;
					settlement.Population -= CalculateCasualties(defenseForce, killRate);

					var defenderMsg = $"We have successfully defended our settlement on Planet #{planet.Id} from attacking {attacker.Name} forces.";
					Notify(defender.Id, defenderMsg, Mood.Good);

					var attackerMsg = $"Our assault on the {defender.Name} settlement on Planet #{planet.Id} failed.";
					Notify(attacker.Id, attackerMsg, Mood.Bad);
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

		private void Notify(int playerId, string message, Mood mood = Mood.Neutral)
		{
			string body = $"Turn {Turn}: {message}";
			GetPlayer(playerId)?.AddMessage(new Message(body, mood));
		}
	}
}
