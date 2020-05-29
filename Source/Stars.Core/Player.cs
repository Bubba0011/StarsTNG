namespace Stars.Core
{
	public class Player
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public Race Race { get; set; } = new Race();

		public IPlayer GetDefaultView() => new DefaultPlayerView(this);
	}
}
