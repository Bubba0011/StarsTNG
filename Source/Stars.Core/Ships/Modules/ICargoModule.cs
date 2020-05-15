using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules
{
    public interface ICargoModule : IShipModule
    {
        public decimal Capacity { get; }
    }
}
