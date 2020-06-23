using System.Collections.Generic;

namespace Stars.Core.Views
{
	public class PlayerPlayerView : IPlayerController
	{
		private readonly Game game;
		private readonly Player player;

		public int Id => player.Id;
		public string? Name => player.Name;
		public IList<Message> Messages => player.GetMessages();

		public PlayerPlayerView(Player player, Game game)
		{
			this.game = game;
			this.player = player;
		}

		public void ClearMessages()
		{
			player.ClearMessages();
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
