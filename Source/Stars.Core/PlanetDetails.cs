namespace Stars.Core
{
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
