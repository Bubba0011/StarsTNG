using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules
{
    public interface IMassDampenerModule : IShipModule
    {
        public decimal DampeningEffect { get; }
    }
}
