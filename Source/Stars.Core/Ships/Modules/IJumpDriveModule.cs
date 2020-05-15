using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules
{
    public interface IJumpDriveModule : IShipModule
    {
        public decimal JumpRange { get; }
    }
}
