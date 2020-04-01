using Stars.Core;
using Stars.Core.Hulls;
using Stars.Core.Interfaces;
using Xunit;

namespace Stars.Tests
{
    public class ShipTests
    {
        [Fact]
        public void ShipHasHull()
        {
            IHull hull = new Warship(HullType.Destroyer);
            var ship = new Ship(hull);
            Assert.NotNull(ship.Hull);
        }
    }
}
