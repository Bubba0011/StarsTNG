namespace Stars.Core
{
	public class Planet
	{
		public int Id { get; set; }
		public Position Position { get; set; }
		public string? Name { get; set; }
		public PlanetDetails? Details { get; set; }
		public Settlement? Settlement { get; set; }

		public IPlanet GetDefaultView() => new DefaultPlanetView(this);
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

		public Environment(int g, int r, int t)
		{
			Gravity = g;
			Radiation = r;
			Temperature = t;
		}

		public override string ToString() => $"G: {Gravity}, R: {Radiation}, T: {Temperature}";
	}

	public struct Minerals
	{
		public int Boranium { get; set; }
		public int Germanium { get; set; }
		public int Ironium { get; set; }

		public Minerals(int br, int gr, int ir)
		{
			Boranium = br;
			Germanium = gr;
			Ironium = ir;
		}

		public override string ToString() => $"Br: {Boranium}, Gr: {Germanium}, Ir: {Ironium}";
	}
}
