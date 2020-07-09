using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	class ScannedFleet : IFleet
	{
		private readonly Fleet fleet;
		private readonly HistoryStore history;
		private readonly int viewingPlayerId;

		public int Id => fleet.Id;
		public int OwnerId => fleet.OwnerId;
		public string? Name => fleet.Name;
		public Position Position => fleet.Position;
		public int ScannerRange => fleet.ScannerRange;
		public bool IsMine => false;
		public string ObjectId => fleet.ObjectId;
		public Velocity? Velocity => fleet.Velocity;
		public Population? Passengers => fleet.Passengers;
		public IEnumerable<Waypoint> Waypoints => new Waypoint[0];
		public IEnumerable<WakePoint> WakePoints => GetWakePoints();

		public ScannedFleet(Fleet fleet, HistoryStore history, int viewingPlayerId)
		{
			this.fleet = fleet;
			this.history = history;
			this.viewingPlayerId = viewingPlayerId;
		}

		private IEnumerable<WakePoint> GetWakePoints()
		{
			return history.GetFleet(Id, viewingPlayerId)
				.OrderBy(h => h.Time)
				.Select(h => new WakePoint(h.Time, h.Position));
		}
	}
}
