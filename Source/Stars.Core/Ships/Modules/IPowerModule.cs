using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules
{
    public interface IPowerModule : IShipModule
    {
        public decimal PowerGeneration { get; }
    }
}
