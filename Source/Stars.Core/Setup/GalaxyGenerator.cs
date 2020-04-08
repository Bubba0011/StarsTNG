using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Setup
{
	public class GalaxyGenerator
	{
		private readonly IRandom rnd;
		private IList<Position> occupiedSpace = new List<Position>();
		const int MAX_TRIES = 1000000;

		public GalaxyGenerator(IRandom? random = null)
		{
			rnd = random ?? new DefaultRandom();
		}

		public Galaxy Generate(GalaxyGeneratorSettings settings)
		{
			// TODO: Validate settings

			Galaxy galaxy = new Galaxy()
			{
				Size = settings.GalaxySize
			};
			try
			{
				galaxy.Planets = Enumerable.Range(0, settings.PlanetCount)
					.Select(_ => RandomPosition(settings.GalaxySize, settings.MinimumDistanceBetweenPlanets))
					.Select(position => new Planet() { Position = position })
					.ToList();
			}
			catch (TimeoutException ex)
			{
				Console.WriteLine(ex);
				throw ex;
			}

			return galaxy;
		}

		private Position RandomPosition(int galaxySize, int minimumDistance)
		{
			const int Padding = 10;
			var tries = 0;

			int Next() => rnd.Next(Padding, galaxySize - Padding);

			Position pos;
			bool isTooClose;
			do
			{
				tries++;

				pos = new Position(Next(), Next());
				isTooClose = occupiedSpace.Any(p => (p.DistanceTo(pos) < minimumDistance));
			} while ((tries < MAX_TRIES) && isTooClose);

			if (tries >= MAX_TRIES)
			{
				throw new TimeoutException("Could not create planets with current settings. Please decrease minimum distance or increase galaxy size.");
			}

			occupiedSpace.Add(pos);

			return pos;
		}
	}
}
