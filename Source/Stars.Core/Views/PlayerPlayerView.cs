namespace Stars.Core.Views
{
	public class PlayerPlayerView : IPlayer
	{
		private readonly Game game;
		private readonly Player player;

		public int Id => player.Id;
		public string? Name => player.Name;

		public PlayerPlayerView(Player player, Game game)
		{
			this.game = game;
			this.player = player;
		}

		public double? GetPlanetValue(IPlanet planet)
		{
			if (planet.Details != null)
			{
				return game.Rules.CalculatePlanetValue(planet.Details, player.Race);
			}
			else
			{
				return null;
			}
		}
	}
}
