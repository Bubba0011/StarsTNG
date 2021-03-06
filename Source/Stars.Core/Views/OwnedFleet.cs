﻿using System.Collections.Generic;
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
		public Velocity? Velocity => fleet.Velocity;
		public Population? Passengers => fleet.Passengers;
		public Speed? MaxSpeed => fleet.MaxSpeed;
		public IEnumerable<Waypoint> Waypoints => fleet.Waypoints ?? new Waypoint[0];
		public IEnumerable<WakePoint> WakePoints => history.GetFleet(Id).OrderBy(h => h.Time).Select(h => new WakePoint(h.Time, h.Position));

		public OwnedFleet(Fleet fleet, HistoryStore history)
		{
			this.fleet = fleet;
			this.history = history;
		}

		public void SetWaypoints(IEnumerable<Waypoint>? waypoints)
		{
			fleet.Waypoints = waypoints?.ToList();
		}

		public void SetName(string name)
		{
			fleet.Name = name;
		}
	}
}
