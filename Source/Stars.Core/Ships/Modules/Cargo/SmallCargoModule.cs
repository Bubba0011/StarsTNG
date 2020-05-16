using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules.Cargo
{
    public class SmallCargoModule : ICargoModule
    {
        public decimal Capacity => 100;

        public decimal Volume => 100;

        public decimal Mass => 5;

        public decimal PowerRequirement => 0;

        public decimal Health => 10;
    }
}
