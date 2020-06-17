using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	class OwnedFleet : IFleetController
	{
		private readonly Fleet fleet;
		private readonly HistoryStore history;

		public int Id => fleet.Id;
		public int OwnerId => fleet.OwnerId;
		public string? Name => fleet.Name;
		public Position Position => fleet.Position;
		public int ScannerRange => fleet.ScannerRange;
		public bool IsMine => true;
		public string ObjectId => fleet.ObjectId;
		public int? Heading => fleet.Heading;
		public IEnumerable<Position> Waypoints => fleet.Waypoints ?? new Position[0];
		public IEnumerable<WakePoint> WakePoints => history.GetFleet(Id).OrderBy(h => h.Turn).Select(h => new WakePoint(h.Turn, h.Position));

		public OwnedFleet(Fleet fleet, HistoryStore history)
		{
			this.fleet = fleet;
			this.history = history;
		}

		public void SetWaypoints(IEnumerable<Position>? waypoints)
		{
			fleet.Waypoints = waypoints?.ToList();
		}

		public void SetName(string name)
		{
			fleet.Name = name;
		}
	}
}
