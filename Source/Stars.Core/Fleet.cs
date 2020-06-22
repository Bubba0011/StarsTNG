using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public class Fleet : IEntity, ISpaceObject
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public Position Position { get; set; }
		public string? Name { get; set; }
		public int ScannerRange { get; set; }
		public string ObjectId => $"Fleet#{Id}";
		public IList<Position>? Waypoints { get; set; }
		public int? Heading { get; set; }
		public Population Passengers { get; set; }

		public IFleet GetDefaultView() => new DefaultFleetView(this);

		public void Move(double speed)
		{
			Heading = CalculateHeading();

			if (Waypoints == null) return;
			if (Waypoints.Any() == false) return;

			var target = Waypoints.First();
			var distanceToTarget = target.DistanceTo(Position);

			if (distanceToTarget > speed)
			{
				var pct = speed / distanceToTarget;
				var targetDelta = target - Position;
				var moveDelta = targetDelta * pct;
				Position += moveDelta;
			}
			else
			{
				Position = target;
				Waypoints.RemoveAt(0);
				if (Waypoints.Any() == false)
				{
					Waypoints = null;
				}
			}
		}

		private int? CalculateHeading()
		{
			if (Waypoints?.Any() == true)
			{
				var delta = Waypoints.First() - Position;
				var radianAngle = Math.Atan2(delta.Y, delta.X);
				var degreeAngle = 180 * radianAngle / Math.PI;
				return (int)Math.Round(degreeAngle, 0);
			}
			else
			{
				return null;
			}
		}
	}
}
