using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Hardpoints
{
    public interface IShipHardpoint
    {
        public HardpointLocation HardpointLocation { get; }
        public HardpointType HardpointType { get; }
    }
}
