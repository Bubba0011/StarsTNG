namespace Stars.Core
{
	public struct Speed
	{
		public double LysPerYear { get; set; }

		public Speed(double lysPerYear)
		{
			LysPerYear = lysPerYear;
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
