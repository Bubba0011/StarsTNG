using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class Galaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();

		public int Size { get; set; }

		public Planet ClosestPlanet(Position target)
		{
			return Planets.OrderBy(i => i.Position.DistanceTo(target)).First();
		}
	}
}
