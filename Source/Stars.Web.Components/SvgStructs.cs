using System.Globalization;

namespace Stars.Web.Components
{
	public struct Vector
	{
		public static readonly Vector Zero = new Vector();

		public double X { get; set; }
		public double Y { get; set; }

		public Vector(double x, double y)
		{
			X = x;
			Y = y;
		}

		public override string ToString() => $"{S(X)} {S(Y)}";

		private static string S(double d) => d.ToString("0.0", CultureInfo.InvariantCulture);
		public static Vector operator +(Vector lhs, Vector rhs) => new Vector(lhs.X + rhs.X, lhs.Y + rhs.Y);
		public static Vector operator -(Vector lhs, Vector rhs) => new Vector(lhs.X - rhs.X, lhs.Y - rhs.Y);
		public static Vector operator *(Vector lhs, double rhs) => new Vector(lhs.X * rhs, lhs.Y * rhs);
		public static Vector operator *(double lhs, Vector rhs) => rhs * lhs;
		public static Vector operator /(Vector lhs, double rhs) => new Vector(lhs.X / rhs, lhs.Y / rhs);
	}

	public struct Rectangle
	{
		public Vector TopLeft { get; set; }
		public Vector Dimensions { get; set; }
		public Vector BottomRight => TopLeft + Dimensions;
		public Vector Center => TopLeft + (Dimensions / 2);
		public double Width => Dimensions.X;
		public double Height => Dimensions.Y;

		public Rectangle(Vector topLeft, Vector dimensions)
		{
			TopLeft = topLeft;
			Dimensions = dimensions;
		}

		public override string ToString() => $"{TopLeft} {Dimensions}";

		public static Rectangle operator /(Rectangle lhs, double rhs)
		{
			return new Rectangle()
			{
				TopLeft = lhs.TopLeft,
				Dimensions = lhs.Dimensions / rhs,
			};
		}
	}
}
