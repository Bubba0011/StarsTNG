using System.Collections.Generic;

namespace Stars.Core
{
	public interface IFleetController : IFleet
	{
		public void SetWaypoints(IEnumerable<Waypoint>? waypoints);
		public void SetName(string name);
	}
}
