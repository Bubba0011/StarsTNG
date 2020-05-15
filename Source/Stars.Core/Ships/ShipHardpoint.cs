using Stars.Core.Ships.Hardpoints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships
{
    public class ShipHardpoint : IShipHardpoint
    {
        public HardpointType HardpointType { get; set; }
        public HardpointLocation HardpointLocation { get; set; }
    }
}
