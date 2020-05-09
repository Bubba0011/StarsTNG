using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Stars.Core
{
	public class Galaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();

		public IList<Player> Players { get; set; } = new List<Player>();

		public int Size { get; set; }

		public Bounds Bounds => new Bounds(Size);

		public Planet ClosestPlanet(Position target)
		{
			return Planets.OrderBy(i => i.Position.DistanceTo(target)).First();
		}
	}

	public struct Bounds
	{
		public int Mid => 0;
		public int Min => -Size / 2;
		public int Max => Size / 2;
		public int Size { get; }

		public Bounds(int size)
		{
			Size = size;
		}
	}
}
