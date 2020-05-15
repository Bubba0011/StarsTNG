using Stars.Core;
using Stars.Core.Interfaces;
using Stars.Core.Ships;
using Xunit;

namespace Stars.Tests
{
    public class ShipTests
    {
        [Fact]
        public void ShipHasClass()
        {
            var ship = ShipFactory.Instance.BuildShip("Zippy");
            Assert.NotNull(ship.ShipClass);
        }

        [Fact]
        public void ShipHasValidClass()
        {
            var ship = ShipFactory.Instance.BuildShip("Zippy");
            Assert.True(ship.ShipClass.IsValid());
        }
    }
}
