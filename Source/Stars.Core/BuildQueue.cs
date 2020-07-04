using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class BuildQueue
	{
		public IList<BuildQueueItem> Items { get; set; } = new List<BuildQueueItem>();

		public BuildResult Build(int availableResources)
		{
			int consumed = 0;
			List<BuildQueueItem> completed = new List<BuildQueueItem>();

			while (availableResources > 0 && Items.Any())
			{
				var item = Items.First();

				int workDone = Math.Min(item.RemainingCost, availableResources);
				item.Invested += workDone;
				availableResources -= workDone;
				consumed += workDone;

				if (item.IsCompleted)
				{
					Items.RemoveAt(0);
					completed.Add(item);
				}
			}

			return new BuildResult(consumed, completed);
		}
	}

	public class BuildResult
	{
		public int ConsumedResources { get; }
		public IEnumerable<BuildQueueItem> CompletedItems { get; }

		public BuildResult(int consumed, IEnumerable<BuildQueueItem> completed)
		{
			ConsumedResources = consumed;
			CompletedItems = completed.ToArray();
		}
	}

	public class BuildQueueItem
	{
		public BuildMenuItem ItemToBuild { get; set; }
		public int Invested { get; set; }

		public int Cost => ItemToBuild.Cost;
		public int RemainingCost => Cost - Invested;
		public bool IsCompleted => RemainingCost <= 0;
		public double Progress => (double)Invested / Cost;

		public BuildQueueItem()
		{
		}

		public BuildQueueItem(BuildMenuItem item)
		{
			ItemToBuild = item;
		}
	}

	public struct BuildMenuItem
	{
		public static readonly BuildMenuItem ScoutShip = new BuildMenuItem("Scout Ship", 25);
		public static readonly BuildMenuItem ColonyShip = new BuildMenuItem("Colony Ship", 15);
		public static readonly BuildMenuItem AssaultShip = new BuildMenuItem("Assault Ship", 30);

		public static readonly BuildMenuItem Garrison = new BuildMenuItem("Garrison", 30);
		public static readonly BuildMenuItem SpacePort = new BuildMenuItem("Space Port", 30);

		public string Name { get; set; }
		public int Cost { get; set; }

		private BuildMenuItem(string name, int cost)
		{
			Name = name;
			Cost = cost;
		}

		internal void Complete(Galaxy galaxy, Planet planet, Settlement settlement, Action<string> notify)
		{
			var item = this;

			if (item.Equals(ScoutShip))
			{
				LaunchShip(50, "Scout");
			}
			else if (item.Equals(ColonyShip))
			{
				int settlers = Math.Min(5000, settlement.Population.Civilians / 2);
				var passengers = new Population(settlers);
				settlement.Population -= passengers;

				var ship = LaunchShip(20, "Mayflower");
				ship.Passengers = passengers;
			}
			else if (item.Equals(AssaultShip))
			{
				int recruites = Math.Min(10_000, settlement.Population.Civilians / 5);
				int marines = recruites / 2;
				settlement.Population -= new Population(recruites);

				var ship = LaunchShip(20, "MEU");
				ship.Passengers = new Population(0, marines);
			}
			else if (item.Equals(Garrison))
			{
				int recruites = Math.Min(10_000, settlement.Population.Civilians / 5);
				int marines = recruites / 2;
				settlement.Population += new Population(-recruites, marines);
			}
			else if (item.Equals(SpacePort))
			{
				settlement.Installations.SpacePort = 200;
				notify($"Space Port completed");
			}

			Fleet LaunchShip(int scanner, string name)
			{
				var ship = new Fleet
				{
					OwnerId = settlement.OwnerId,
					Position = planet.Position,
					ScannerRange = scanner,
				};

				galaxy.Fleets.Add(ship);
				ship.Name = $"{name} #{ship.Id}";

				notify($"Ship launched - {ship.Name}");

				return ship;
			}
		}

		public static IEnumerable<BuildMenuItem> GetBuildableItems(ISettlement settlement)
		{
			yield return ScoutShip;
			yield return ColonyShip;

			if (settlement.Installations?.SpacePort == 0)
			{
				yield return SpacePort;
			}
		}
	}
}
