using System.Globalization;

namespace Stars.Web.Components
{
	static class F
	{
		public static string S(double d) => d.ToString("0.0", CultureInfo.InvariantCulture);
	}

	public struct SvgDims
	{
		public double Width { get; set; }
		public double Height { get; set; }

		public SvgDims(double width, double height)
		{
			Width = width;
			Height = height;
		}

		public override string ToString() => $"{F.S(Width)} {F.S(Height)}";

		public static SvgDims operator /(SvgDims lhs, double rhs) => new SvgDims(lhs.Width / rhs, lhs.Height / rhs);
	}

	public struct SvgPos
	{
		public static readonly SvgPos Zero = new SvgPos();

		public double X { get; set; }
		public double Y { get; set; }

		public SvgPos(double x, double y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"{F.S(X)} {F.S(Y)}";

		public static SvgPos operator +(SvgPos lhs, SvgDims rhs) => new SvgPos(lhs.X + rhs.Width, lhs.Y + rhs.Height);
		public static SvgPos operator -(SvgPos lhs, SvgDims rhs) => new SvgPos(lhs.X - rhs.Width, lhs.Y - rhs.Height);
		public static SvgDims operator -(SvgPos lhs, SvgPos rhs) => new SvgDims(lhs.X - rhs.X, lhs.Y - rhs.Y);
	}

	public struct SvgRect
	{
		public SvgPos TopLeft { get; set; }
		public SvgDims Dims { get; set; }
		public SvgPos Center => TopLeft + (Dims / 2);

		public SvgRect(SvgPos topLeft, SvgDims dims)
		{
			TopLeft = topLeft;
			Dims = dims;
		}

		public override string ToString() => $"{TopLeft} {Dims}";

		public static SvgRect operator /(SvgRect lhs, double rhs)
		{
			return new SvgRect()
			{
				TopLeft = lhs.TopLeft,
				Dims = lhs.Dims / rhs,
			};
		}
	}
}
