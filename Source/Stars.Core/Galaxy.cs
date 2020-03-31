using System.Collections.Generic;

namespace Stars.Core
{
	public class Galaxy
	{
		public IList<Planet> Planets { get; set; } = new List<Planet>();
	}
}
