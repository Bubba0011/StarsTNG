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

		public string Name { get; set; }
		public int Cost { get; set; }

		private BuildMenuItem(string name, int cost)
		{
			Name = name;
			Cost = cost;
		}
	}
}
