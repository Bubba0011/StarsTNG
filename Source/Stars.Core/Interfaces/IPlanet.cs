namespace Stars.Core
{
	public interface IPlanet : ISpaceObject
	{
		int Id { get; }
		string? Name { get; }
		PlanetDetails? Details { get; }
		ISettlement? Settlement { get; }
		int? Timestamp { get => null; }
	}
}
