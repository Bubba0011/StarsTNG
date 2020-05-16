using Stars.Core.Ships.Hardpoints;
using Stars.Core.Ships.Hulls;
using Stars.Core.Ships.Modules;
using Stars.Core.Ships.Modules.Power;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stars.Core.Ships
{
    public class ShipFactory
    {
        private Dictionary<string, HullType> Hulls;
        private Dictionary<string, ShipClass> ShipClasses;
        
        private ShipFactory()
        {
            Hulls = new Dictionary<string, HullType>();
            ShipClasses = new Dictionary<string, ShipClass>();

            InitializeHulls();
            InitializeShipClasses();
        }

        private void InitializeShipClasses()
        {
            ShipClasses.Add("Zippy", new ShipClass("Zippy", Hulls["Scout"])
            {
                Modules = new List<IShipModule>()
                {
                    new SmallPowerModule()
                },
                Weapons = new List<IShipHardpoint>()
                {
                    new SmallFrontLaser()
                }
            });
        }

        private void InitializeHulls()
        {
            Hulls.Add("Destroyer",
                new HullType("Destroyer")
                {
                    BaseMass = 1000,
                    ModuleCapacity = 1000,
                    Hardpoints = new List<IShipHardpoint>()
                    {
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Large },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Top, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Left, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Right, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Rear, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Rear, HardpointType = HardpointType.Small },
                    }
                }
            );

            Hulls.Add("Battleship",
                new HullType("Battleship")
                {
                    BaseMass = 700,
                    ModuleCapacity = 700,
                    Hardpoints = new List<IShipHardpoint>()
                    {
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Top, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Bottom, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Rear, HardpointType = HardpointType.Small }
                    }
                }
            );

            Hulls.Add("Cruiser",
                new HullType("Cruiser")
                {
                    BaseMass = 300,
                    ModuleCapacity = 300,
                    Hardpoints = new List<IShipHardpoint>()
                    {
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Medium },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Left, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Right, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Top, HardpointType = HardpointType.Small },
                    }
                }
            );

            Hulls.Add("Scout",
                new HullType("Scout")
                {
                    BaseMass = 100,
                    ModuleCapacity = 100,
                    Hardpoints = new List<IShipHardpoint>()
                    {
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Small },
                        new ShipHardpoint() { HardpointLocation = HardpointLocation.Front, HardpointType = HardpointType.Small },
                    }
                }
            );
        }

        public Ship BuildShip(string shipClass)
        {
            return new Ship(ShipClasses[shipClass]);
        }

        public static ShipFactory Instance = new ShipFactory();
    }
}
