using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Setup
{
	public class GalaxyGenerator
	{
		const int MAX_TRIES = 1_000_000;

		private readonly IRandom rnd;

		public GalaxyGenerator(IRandom? random = null)
		{
			rnd = random ?? new DefaultRandom();
		}

		public Galaxy Generate(GalaxyGeneratorSettings settings)
		{
			// TODO: Validate settings

			if (CalculatePlanetCapacity(settings) < settings.PlanetCount)
			{
				throw new OutOfSpaceException($"Not enough space for planets in the galaxy.");
			}

			Galaxy galaxy = new Galaxy()
			{
				Size = settings.GalaxySize
			};

			galaxy.Planets = RandomPositions(settings)
				.Take(settings.PlanetCount)
				.Select(position => new Planet() { Position = position })
				.ToList();

			return galaxy;
		}

		private int CalculatePlanetCapacity(GalaxyGeneratorSettings settings)
		{
			static long Square(long n) => n * n;

			var spaceAvailable = Square(settings.GalaxySize);
			var spaceRequiredPerPlanet = Square(settings.MinimumDistanceBetweenPlanets);

			return (int)(spaceAvailable / spaceRequiredPerPlanet);
		}

		private IEnumerable<Position> RandomPositions(GalaxyGeneratorSettings settings)
		{
			List<Position> occupiedSpace = new List<Position>();

			while (true)
			{
				var tries = 0;
				Position pos;
				bool isTooClose;

				do
				{
					pos = RandomPosition();
					tries++;
					isTooClose = occupiedSpace.Any(p => (p.DistanceTo(pos) < settings.MinimumDistanceBetweenPlanets));
				} while (isTooClose && tries < MAX_TRIES);

				if (tries >= MAX_TRIES)
				{
					throw new OutOfSpaceException("Could not create planets with current settings. Please decrease minimum distance or increase galaxy size.");
				}

				occupiedSpace.Add(pos);
				yield return pos;
			}

			Position RandomPosition()
			{
				int Next() => rnd.Next(settings.Padding, settings.GalaxySize - settings.Padding);

				return new Position(Next(), Next());
			}
		}
	}
}
