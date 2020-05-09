namespace Stars.Core
{
	public interface IPlanet
	{
		public int Id { get; }
		public Position Position { get; }
		public string? Name { get; }
		public PlanetDetails? Details { get; }
		public int? OwnerId { get; }
	}
}
