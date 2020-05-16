namespace Stars.Core
{
	public struct GalaxyBounds
	{
		public int Mid => 0;
		public int Min => -Size / 2;
		public int Max => Size / 2;
		public int Size { get; set; }

		public GalaxyBounds(int size)
		{
			Size = size;
		}
	}
}
