using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships.Modules.Power
{
    public class SmallPowerModule : IPowerModule
    {
        public decimal PowerGeneration => 100;

        public decimal Volume => 10;

        public decimal Mass => 10;

        public decimal PowerRequirement => 0;

        public decimal Health => 10;
    }
}
