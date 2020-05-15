using Stars.Core.Ships.Hardpoints;
using Stars.Core.Ships.Hulls;
using Stars.Core.Ships.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stars.Core.Ships
{
    /// <summary>
    /// A Ship Class is a particular configuration of a hull and modules/hard points.
    /// Works much like a blueprint to build ships from.
    /// </summary>
    public class ShipClass
    {
        public string Name;
        public HullType HullType;
        public List<IShipModule> Modules;
        public List<IShipHardpoint> Weapons;

        public ShipClass(string name, HullType hullType)
        {
            Name = name;
            HullType = hullType;
            Modules = new List<IShipModule>();
            Weapons = new List<IShipHardpoint>();
        }

        public void Add(IShipModule module)
        {
            Modules.Add(module);
        }

        public void Remove(IShipModule module)
        {
            Modules.Remove(module);
        }

        public bool IsValid()
        {
            decimal moduleVolume = 0;
            decimal powerRequirement = 0;
            decimal powerGeneration = 0;

            foreach (var module in Modules)
            {
                powerRequirement += module.PowerRequirement;
                moduleVolume += module.Volume;
                if (module is IPowerModule)
                {
                    powerGeneration += ((IPowerModule)module).PowerGeneration;
                }
            }

            List<IShipHardpoint> available = new List<IShipHardpoint>(HullType.Hardpoints);
            bool weaponsValid = true;
            foreach (var weapon in Weapons)
            {
                var h = available.FirstOrDefault(h => h.HardpointLocation == weapon.HardpointLocation && h.HardpointType == weapon.HardpointType);
                if (h is null)
                {
                    weaponsValid = false;
                }
                else
                {
                    available.Remove(h);
                }
            }

            return moduleVolume <= HullType.ModuleCapacity && powerRequirement < powerGeneration && weaponsValid;
        }
    }
}
