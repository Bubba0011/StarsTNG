namespace Stars.Core
{
    public class Ship
    {
        public Position Position { get; set; }
        public string? Name { get; set; }
        public Hull Hull { get; set; }

        public Ship(Hull hull)
        {
            Hull = hull;
        }
    }
}
