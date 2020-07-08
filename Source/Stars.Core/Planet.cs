namespace Stars.Core
{
	public class Planet : IEntity, ISpaceObject
	{
		public int Id { get; set; }
		public Position Position { get; set; }
		public string? Name { get; set; }
		public PlanetDetails Details { get; set; } = new PlanetDetails();
		public Settlement? Settlement { get; set; }
		public string ObjectId => $"Planet#{Id}";
	}
}
