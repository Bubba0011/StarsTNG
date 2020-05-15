using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Hardpoints
{
    public class SmallFrontLaser : IShipHardpoint
    {
        public HardpointType HardpointType => HardpointType.Small;
        public HardpointLocation HardpointLocation => HardpointLocation.Front;

        public decimal Volume => 2;

        public decimal Mass => 5;

        public decimal PowerRequirement => 20;

        public decimal Health => 10;
    }
}
