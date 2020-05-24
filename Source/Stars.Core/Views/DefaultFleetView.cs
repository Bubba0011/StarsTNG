using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
	class DefaultFleetView : IFleet
	{
		private readonly Fleet fleet;

		public int Id => fleet.Id;
		public int OwnerId => fleet.OwnerId;
		public string? Name => fleet.Name;
		public Position Position => fleet.Position;
		public int ScannerRange => fleet.ScannerRange;
		public bool IsMine => false;
		public string ObjectId => fleet.ObjectId;
		public IEnumerable<Position> Waypoints => Enumerable.Empty<Position>();

		public DefaultFleetView(Fleet fleet)
		{
			this.fleet = fleet;
		}
	}
}
