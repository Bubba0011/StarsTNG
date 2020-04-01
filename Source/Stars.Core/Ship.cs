using Stars.Core.Interfaces;

namespace Stars.Core
{
    public class Ship
    {
        public Position Position { get; set; }
        public string? Name { get; set; }
        public IHull Hull { get; set; }

        public Ship(IHull hull)
        {
            Hull = hull;
        }
    }
}
