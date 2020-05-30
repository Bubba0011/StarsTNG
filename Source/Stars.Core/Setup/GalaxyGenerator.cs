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
				Bounds = new GalaxyBounds(settings.GalaxySize)
			};

			var planets = RandomPositions(settings)
				.Take(settings.PlanetCount)
				.Select(CreatePlanet);

			galaxy.Planets.AddRange(planets);

			return galaxy;
		}

		private Planet CreatePlanet(Position position)
		{
			var planet = new Planet()
			{
				Position = position,
				Details = CreateDetails(),
			};

			return planet;
		}

		private PlanetDetails CreateDetails()
		{
			var details = new PlanetDetails()
			{
				Environment = new Environment()
				{
					Gravity = rnd.Norm(0, 100, 3),
					Radiation = rnd.Norm(0, 100, 3),
					Temperature = rnd.Norm(0, 100, 3),
				},

				Minerals = new Minerals()
				{
					Boranium = rnd.Norm(0, 100, 3),
					Germanium = rnd.Norm(0, 100, 3),
					Ironium = rnd.Norm(0, 100, 3),
				},
			};

			return details;
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
				int Next() => rnd.Next(settings.Padding - settings.GalaxySize/2, settings.GalaxySize/2 - settings.Padding);

				return new Position(Next(), Next());
			}
		}
	}
}
