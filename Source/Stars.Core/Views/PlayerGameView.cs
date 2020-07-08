using System.Collections.Generic;
using System.Linq;

namespace Stars.Core.Views
{
	public class PlayerGameView
	{
		private readonly Game game;
		private readonly int playerId;
		private readonly List<IPlayer> players;

		public StarDate Time => game.Time;
		public IEnumerable<PlayerScore> Scoreboard => game.Scoreboard;
		public IGalaxy Galaxy { get; }
		public IEnumerable<IPlayer> Players => players;

		public PlayerGameView(Game game, int playerId)
		{
			this.game = game;
			this.playerId = playerId;

			Galaxy = new PlayerGalaxyView(game, playerId);
			players = game.Players.Select(Project).ToList();
		}

		private IPlayer Project(Player player)
		{
			return player.Id == playerId
				? new PlayerPlayerView(player, game)
				: player.GetDefaultView();
		}
	}
}
