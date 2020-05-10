namespace Stars.Core
{
	public class Planet : IPlanet
	{
		public int Id { get; set; }
		public Position Position { get; set; }
		public string? Name { get; set; }
		public PlanetDetails? Details { get; set; }
		public Settlement? Settlement { get; set; }

		ISettlement? IPlanet.Settlement => Settlement;
	}

	public class PlanetDetails
	{
		public Environment Environment { get; set; }
		public Minerals Minerals { get; set; }
	}

	public struct Environment
	{
		public int Gravity { get; set; }
		public int Radiation { get; set; }
		public int Temperature { get; set; }

		public override string ToString() => $"G: {Gravity}, R: {Radiation}, T: {Temperature}";
	}

	public struct Minerals
	{
		public int Boranium { get; set; }
		public int Germanium { get; set; }
		public int Ironium { get; set; }

		public override string ToString() => $"Br: {Boranium}, Gr: {Germanium}, Ir: {Ironium}";
	}
}
