using System;

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
	}
}
