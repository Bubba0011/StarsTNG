using System;
using System.Linq;

namespace Stars.Core.Setup
{
	class DefaultRandom : IRandom
	{
		private readonly Random rnd;

		public DefaultRandom(int? seed = null)
		{
			rnd = seed.HasValue ? new Random(seed.Value) : new Random();
		}

		public int Next(int minValue, int maxValue)
		{
			return rnd.Next(minValue, maxValue);
		}

		public int Norm(int minValue, int maxValue, int count)
		{
			return (int)Enumerable.Range(1, count)
				.Select(_ => Next(minValue, maxValue))
				.Average();
		}
	}
}
