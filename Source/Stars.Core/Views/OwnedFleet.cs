using System.Collections.Generic;

namespace Stars.Core
{
	class OwnedFleet : IFleet
	{
		private readonly Fleet fleet;

		public int Id => fleet.Id;
		public int OwnerId => fleet.OwnerId;
		public string? Name => fleet.Name;
		public Position Position => fleet.Position;
		public int ScannerRange => fleet.ScannerRange;
		public bool IsMine => true;
		public string ObjectId => fleet.ObjectId;
		public IEnumerable<Position> Waypoints => fleet.Waypoints ?? new Position[0];

		public OwnedFleet(Fleet fleet)
		{
			this.fleet = fleet;
		}
	}
}
