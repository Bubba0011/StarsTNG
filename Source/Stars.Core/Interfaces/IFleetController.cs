using System.Collections.Generic;

namespace Stars.Core
{
	public interface IFleetController : IFleet
	{
		public void SetWaypoints(IEnumerable<Position>? waypoints);
	}
}
