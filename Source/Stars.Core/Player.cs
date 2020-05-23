namespace Stars.Core
{
	public class Player
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		public IPlayer GetDefaultView() => new DefaultPlayerView(this);
	}
}
