using Stars.Core;
using System;

namespace Stars.Web.Lab.Data
{
	public class GameClient : IDisposable
	{
		private readonly GameServer server;

		public int PlayerId { get; }
		public IGalaxy GalaxyView { get; private set; }

		public EventHandler Updated;

		public GameClient(GameServer server, int playerId)
		{
			this.server = server;
			server.GameUpdated += OnGameUpdated;

			PlayerId = playerId;
			GalaxyView = new PlayerView(server.Game.Galaxy, playerId);
		}

		public void Dispose()
		{
			server.GameUpdated -= OnGameUpdated;
		}

		private void OnGameUpdated(object sender, EventArgs e)
		{
			GalaxyView = new PlayerView(server.Game.Galaxy, PlayerId);
			Updated?.Invoke(this, e);
		}
	}
}
