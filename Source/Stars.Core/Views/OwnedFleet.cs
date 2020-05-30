using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	class OwnedFleet : IFleetController
	{
		private readonly Fleet fleet;

		public int Id => fleet.Id;
		public int OwnerId => fleet.OwnerId;
		public string? Name => fleet.Name;
		public Position Position => fleet.Position;
		public int ScannerRange => fleet.ScannerRange;
		public bool IsMine => true;
		public string ObjectId => fleet.ObjectId;
		public int? Heading => fleet.Heading;
		public IEnumerable<Position> Waypoints => fleet.Waypoints ?? new Position[0];

		public OwnedFleet(Fleet fleet)
		{
			this.fleet = fleet;
		}

		public void SetWaypoints(IEnumerable<Position>? waypoints)
		{
			fleet.Waypoints = waypoints?.ToList();
		}
	}
}
