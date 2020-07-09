namespace Stars.Core
{
	public struct Speed
	{
		public decimal WarpFactor { get; set; }
		public double LysPerYear => (double)(WarpFactor * WarpFactor);

		public Speed(decimal warpFactor)
		{
			WarpFactor = warpFactor;
		}

		public override string ToString()
		{
			return $"Warp {WarpFactor}";
		}

		public static double operator *(Speed speed, Duration time)
		{
			return speed.LysPerYear * time.YearFraction;
		}

		public static double operator *(Duration time, Speed speed)
		{
			return speed * time;
		}

		public static Duration operator /(double distance, Speed speed)
		{
			var years = distance / speed.LysPerYear;
			return Duration.FromYears(years);
		}
	}
}
