namespace Stars.Core
{
	class DefaultPlayerView : IPlayer
	{
		private readonly Player player;

		public int Id => player.Id;
		public string? Name => player.Name;

		public DefaultPlayerView(Player player)
		{
			this.player = player;
		}
	}
}
