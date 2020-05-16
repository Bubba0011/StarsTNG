using Stars.Core.Interfaces;
using Stars.Core.Ships;
using Stars.Core.Ships.Modules;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Core
{
    public class Ship
    {
        public Position Position { get; set; }
        public string? Name { get; set; }
        public ShipClass ShipClass { get; set; }
        public decimal CargoMass { get; internal set; }
        public decimal HullHealth { get; internal set; }
        public List<IShipModule> Modules;

        /// <summary>
        /// Inertia is used to calculate turn radius in combat and affects the speed of travel during a jump.
        /// </summary>
        public decimal Inertia
        {
            get
            {
                decimal totalMass = ShipClass.HullType.BaseMass + CargoMass;
                decimal dampeningMultiplier = 1;
                foreach (var module in Modules)
                {
                    totalMass += module.Mass;

                    if (module is IMassDampenerModule)
                    {
                        dampeningMultiplier *= ((IMassDampenerModule)module).DampeningEffect * module.Health;
                    }
                }

                return totalMass * dampeningMultiplier;
            }
        }

        /// <summary>
        /// Raw jump range of the ship.
        /// </summary>
        public decimal JumpRange
        {
            get
            {
                return Modules.Where(m => m is IJumpDriveModule)?.Max(m => ((IJumpDriveModule)m)?.JumpRange * m?.Health) ?? 0;
            }
        }

        public Ship(ShipClass shipClass)
        {
            ShipClass = shipClass;
            Modules = new List<IShipModule>(shipClass.Modules); // Clone to get our own copy for health purposes
            HullHealth = 1;
        }
    }
}
