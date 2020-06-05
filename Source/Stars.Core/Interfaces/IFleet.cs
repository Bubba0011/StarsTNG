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
		int? Heading { get; }
		IEnumerable<Position> Waypoints { get; }
		IEnumerable<WakePoint> WakePoints { get => Enumerable.Empty<WakePoint>(); }
	}

	public struct WakePoint
	{
		public Position Position { get; }
		public int Turn { get; }

		public WakePoint(int turn, Position position)
		{
			this.Turn = turn;
			this.Position = position;
		}
	}
}
