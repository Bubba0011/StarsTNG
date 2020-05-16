using Stars.Core.Ships.Hardpoints;
using System.Collections.Generic;

namespace Stars.Core.Ships.Hulls
{
    /// <summary>
    /// A Hull Type is a core skeleton of a ship, and has a capacity to hold a certain volume
    /// of modules and has a set amount of hardpoints in fixed locations.
    /// </summary>
    public class HullType
    {
        public string Name { get; }
        public decimal BaseMass { get; set; }
        public decimal ModuleCapacity { get; set; }
        public List<IShipHardpoint> Hardpoints { get; set; }

        public HullType(string name)
        {
            Name = name;
            Hardpoints = new List<IShipHardpoint>();
        }
    }
}
