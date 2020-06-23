using System.Collections.Generic;

namespace Stars.Core
{
	public interface IPlayer
	{
		int Id { get; }
		string? Name { get; }
		double? GetPlanetValue(IPlanet planet);
		IList<Message> Messages { get; }
	}
}
