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
		public IList<Waypoint>? Waypoints { get; set; }
		public Velocity? Velocity { get; set; }
		public Population Passengers { get; set; }
		public Speed MaxSpeed { get; set; } = new Speed(49);

		public void Move(Duration time)
		{
			var speed = MaxSpeed;

			if (Waypoints?.Any() == true)
			{
				var delta = Waypoints.First().Position - Position;
				Velocity = new Velocity(speed, delta);
			}
			else
			{
				Velocity = null;
				return;
			}

			var target = Waypoints.First().Position;
			var distanceToTarget = target.DistanceTo(Position);
			var distanceCovered = speed * time;

			if (distanceToTarget > distanceCovered)
			{
				var pct = distanceCovered / distanceToTarget;
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
	}
}
