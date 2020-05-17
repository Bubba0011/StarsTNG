using Stars.Core;
using System;

namespace Stars.Web.Lab.Data
{
	public class GameClient : IDisposable
	{
		private readonly GameServer server;

		public int PlayerId { get; }
		public IGalaxy GalaxyView { get; private set; }
		public string TriggerStatus { get; private set; }
		public int CurrentTurn => server.Game.Turn;

		public EventHandler GameUpdated;
		public EventHandler TriggerUpdated;

		public GameClient(GameServer server, int playerId)
		{
			this.server = server;
			server.GameUpdated += OnGameUpdated;
			server.TriggerChanged += OnTriggerUpdated;

			PlayerId = playerId;
			GalaxyView = server.GetPlayerView(this);
		}

		public void Dispose()
		{
			server.TriggerChanged -= OnTriggerUpdated;
			server.GameUpdated -= OnGameUpdated;
		}

		private void OnGameUpdated(object sender, EventArgs e)
		{
			GalaxyView = server.GetPlayerView(this);
			GameUpdated?.Invoke(this, e);
		}

		private void OnTriggerUpdated(object sender, string status)
		{
			TriggerStatus = status;
			TriggerUpdated?.Invoke(this, EventArgs.Empty);
		}
	}
}
