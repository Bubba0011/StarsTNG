using Stars.Core.Interfaces;

namespace Stars.Core.Ships.Hulls
{
    public class Warship : IHull
    {
        public HullType Type { get; set; }

        public Warship(HullType type)
        {
            Type = type;
        }

        public override string ToString() => $"Warship: {Type}";
    }
}
