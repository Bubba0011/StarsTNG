using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Setup
{
	public class GalaxyGenerator
	{
		private Random rnd = new Random();
		private IList<Position> occupiedSpace = new List<Position>();

		public Galaxy Generate(GalaxyGeneratorSettings settings)
		{
			// TODO: Validate settings

			Galaxy galaxy = new Galaxy()
			{
				Size = settings.GalaxySize
			};

			galaxy.Planets = Enumerable.Range(0, settings.PlanetCount)
				.Select(_ => RandomPosition(settings.GalaxySize, settings.MinimumDistanceBetweenPlanets))
				.Select(position => new Planet() { Position = position })
				.ToList();

			return galaxy;
		}

		private Position RandomPosition(int galaxySize, int minimumDistance)
		{
			const int Padding = 10;

			int Next() => rnd.Next(Padding, galaxySize - Padding);

			Position position;
			bool isToClose;
			do
			{
				position = new Position(Next(), Next());
				isToClose = occupiedSpace.Any(occupiedPosition => IsInsideMinimumDistance(occupiedPosition, position, minimumDistance));
			} while (isToClose);

			occupiedSpace.Add(position);

			return position;
		}

		private bool IsInsideMinimumDistance(Position p1, Position p2, int minimumDistance)
		{
			return p1.DistanceTo(p2) < minimumDistance;
		}
	}
}
