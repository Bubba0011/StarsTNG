using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class BuildQueue
	{
		public IList<BuildQueueItem> Items { get; set; } = new List<BuildQueueItem>();

		public IEnumerable<BuildQueueItem> Build(int availableResources)
		{
			while (availableResources > 0 && Items.Any())
			{
				var item = Items.First();

				int workDone = Math.Min(item.RemainingCost, availableResources);
				item.Invested += workDone;
				availableResources -= workDone;

				if (item.IsCompleted)
				{
					Items.RemoveAt(0);
					yield return item;
				}
			}
		}
	}

	public class BuildQueueItem
	{
		public const string Nada = "";
		public const string ScoutShip = "Scout Ship";
		public const string ColonyShip = "Colony Ship";

		public string ItemToBuild { get; set; }
		public int Cost { get; set; }
		public int Invested { get; set; }

		public int RemainingCost => Cost - Invested;
		public bool IsCompleted => RemainingCost <= 0;
		public double Progress => (double)Invested / Cost;

		public BuildQueueItem()
		{
			ItemToBuild = Nada;
		}

		public BuildQueueItem(string item, int cost)
		{
			ItemToBuild = item;
			Cost = cost;
		}
	}
}
