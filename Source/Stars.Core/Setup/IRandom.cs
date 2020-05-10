using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Setup
{
	public interface IRandom
	{
		int Next(int minValue, int maxValue);

		int Norm(int minValue, int maxValue, int count);
	}

	public static class IRandomExtensions
	{
		public static T PickOne<T>(this IRandom random, IEnumerable<T> values)
		{
			int count = values.Count();
			int pick = random.Next(0, count);

			return values.Skip(pick).First();
		}
	}
}
