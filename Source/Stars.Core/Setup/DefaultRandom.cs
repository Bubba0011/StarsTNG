using System;

namespace Stars.Core.Setup
{
	class DefaultRandom : IRandom
	{
		private Random rnd = new Random();

		public int Next(int minValue, int maxValue)
		{
			return rnd.Next(minValue, maxValue);
		}
	}
}
