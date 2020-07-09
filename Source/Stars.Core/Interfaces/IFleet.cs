using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	public interface IFleet : ISpaceObject
	{
		int Id { get; }
		int OwnerId { get; }
		string? Name { get; }
		int ScannerRange { get; }
		bool IsMine { get; }
		Velocity? Velocity { get; }
		Population? Passengers { get; }
		Speed? MaxSpeed { get; }
		IEnumerable<Waypoint> Waypoints { get; }
		IEnumerable<WakePoint> WakePoints { get => Enumerable.Empty<WakePoint>(); }
	}

	public struct WakePoint
	{
		public Position Position { get; }
		public StarDate Time { get; }

		public WakePoint(StarDate time, Position position)
		{
			Time = time;
			Position = position;
		}
	}
}
