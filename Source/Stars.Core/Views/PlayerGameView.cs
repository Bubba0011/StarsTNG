using System.Collections.Generic;

namespace Stars.Core.Views
{
	public class PlayerGameView
	{
		private readonly Game game;
		private readonly int playerId;

		public int Turn => game.Turn;
		public IEnumerable<PlayerScore> Scoreboard => game.Scoreboard;
		public IGalaxy Galaxy { get; }

		public PlayerGameView(Game game, int playerId)
		{
			this.game = game;
			this.playerId = playerId;

			Galaxy = new PlayerGalaxyView(game, playerId);
		}
	}
}
