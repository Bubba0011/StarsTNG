﻿using System;

namespace Stars.Core
{
	public class GameRules
	{
		public int TimeStep => 1;

		public double CalculatePlanetValue(PlanetDetails planetDetails, Race race)
		{
			static double F(int actual, int preferred)
			{
				const int zone = 20;

				int delta = Math.Abs(actual - preferred);

				return delta < zone
					? Math.Pow((double)(zone - delta) / zone, 2)
					: -10;
			}

			var actual = planetDetails.Environment;
			var preferred = race.EnvironmentPreferences;

			var g = F(actual.Gravity, preferred.Gravity);
			var r = F(actual.Radiation, preferred.Radiation);
			var t = F(actual.Temperature, preferred.Temperature);

			var grt = g + r + t;

			return grt > 0 
				? Math.Sqrt(grt) / Math.Sqrt(3) 
				: 0;
		}

		public int CalculatePopulationGrowth(Planet planet, Player owner)
		{
			var settlement = planet.Settlement;
			if (settlement == null)
			{
				return 0;
			}

			int basePopulationCapacity = owner.Race.PlanetPopulationCapacity;
			double baseGrowthRate = owner.Race.PopulationGrowthRate;
			double planetHabFactor = CalculatePlanetValue(planet.Details, owner.Race);

			double effectivePopulationCapacity = basePopulationCapacity * planetHabFactor;
			double capacityPct = settlement.Population / effectivePopulationCapacity;

			double crowdingFactor = capacityPct <= 0.25 ? 1 : 16.0 / 9 * Math.Pow(1 - capacityPct, 2);
			double effectiveGrowthRate = baseGrowthRate * planetHabFactor * crowdingFactor;

			return (int)Math.Round(settlement.Population * effectiveGrowthRate);
		}
	}
}
