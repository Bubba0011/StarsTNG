using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships
{
    public interface IShipModule
    {
        public decimal Volume { get; }
        public decimal Mass { get; }
        public decimal PowerRequirement { get; }

        public decimal Health { get; }
    }
}
