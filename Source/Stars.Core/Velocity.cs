using System;

namespace Stars.Core
{
	public struct Velocity
	{
		public int Heading { get; set; }
		public Speed Speed { get; set; }

		public Velocity(Speed speed, Position delta)
		{
			Speed = speed;

			var radianAngle = Math.Atan2(delta.Y, delta.X);
			var degreeAngle = 180 * radianAngle / Math.PI;
			Heading = (int)Math.Round(degreeAngle, 0);
		}
	}
}
