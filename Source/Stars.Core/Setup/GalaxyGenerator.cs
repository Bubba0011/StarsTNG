using System;
using System.Linq;

namespace Stars.Core.Setup
{
	public class GalaxyGenerator
	{
		private Random rnd = new Random();

		public Galaxy Generate(GalaxyGeneratorSettings settings)
		{
			// TODO: Validate settings

			Galaxy galaxy = new Galaxy();

			galaxy.Planets = Enumerable.Range(0, settings.PlanetCount)
				.Select(_ => RandomPosition(settings.GalaxySize))
				.Select(position => new Planet() { Position = position })
				.ToList();

			return galaxy;
		}

		private Position RandomPosition(int galaxySize)
		{
			const int Padding = 10;

			int Next() => rnd.Next(Padding, galaxySize - Padding);

			return new Position(Next(), Next());
		}
	}
}
