using Stars.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Infrastructure.Data
{
	struct GalaxyStats
	{
		public Position Center { get; set; }

		public int Min { get; set; }
		public int Max { get; set; }
		public int Avg { get; set; }

		public Planet MinPlanet { get; set; }
		public Planet MaxPlanet { get; set; }

		public static GalaxyStats Create(Galaxy galaxy)
		{
			var stats = new GalaxyStats();

			// Center of galaxy
			var pts = galaxy.Planets.Select(planet => planet.Position);
			var cx = Round(pts.Average(p => p.X));
			var cy = Round(pts.Average(p => p.Y));
			stats.Center = new Position(cx, cy);

			// Distance to closest neighbor
			var closestNeighborDistances = galaxy.Planets
				.Select(planet => Closest(planet, galaxy.Planets))
				.ToList();

			var min = closestNeighborDistances.Min();
			var max = closestNeighborDistances.Max();

			stats.Min = Round(min);
			stats.Max = Round(max);
			stats.Avg = Round(closestNeighborDistances.Average());

			var iMin = closestNeighborDistances.IndexOf(min);
			var iMax = closestNeighborDistances.IndexOf(max);

			stats.MinPlanet = galaxy.Planets[iMin];
			stats.MaxPlanet = galaxy.Planets[iMax];

			return stats;

			static int Round(double x)
			{
				return (int)Math.Round(x, 0);
			}

			static double Closest(Planet planet, IList<Planet> planets)
			{
				var minDistSqrd = planets
					.Where(p => p != planet)
					.Select(p => DistanceSquared(p.Position, planet.Position))
					.Min();

				return Math.Sqrt(minDistSqrd);

				static double DistanceSquared(Position a, Position b)
				{
					var dx = a.X - b.X;
					var dy = a.Y - b.Y;
					return dx * dx + dy * dy;
				}
			}
		}
	}
}
