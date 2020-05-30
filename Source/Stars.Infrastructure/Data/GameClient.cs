using Stars.Core;
using Stars.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Stars.Infrastructure.Data
{
	public class GameClient : IDisposable
	{
		private readonly GameServer server;
		private PlayerGameView gameView;

		public int PlayerId { get; }
		public IGalaxy GalaxyView => gameView.Galaxy;
		public string TriggerStatus { get; private set; }
		public int CurrentTurn => gameView.Turn;
		public IEnumerable<PlayerScore> Scoreboard => gameView.Scoreboard;
		public IEnumerable<IPlayer> Players => gameView.Players;

		public EventHandler GameUpdated;
		public EventHandler TriggerUpdated;

		public GameClient(GameServer server, int playerId)
		{
			this.server = server;
			server.GameUpdated += OnGameUpdated;
			server.TriggerChanged += OnTriggerUpdated;

			PlayerId = playerId;
			gameView = server.GetPlayerView(this);
		}

		public void SetReadyFlag()
		{
			server.SetPlayerReadyFlag(PlayerId);
		}

		public void Dispose()
		{
			server.TriggerChanged -= OnTriggerUpdated;
			server.GameUpdated -= OnGameUpdated;
		}

		private void OnGameUpdated(object sender, EventArgs e)
		{
			gameView = server.GetPlayerView(this);
			GameUpdated?.Invoke(this, e);
		}

		private void OnTriggerUpdated(object sender, TriggerEventArgs e)
		{
			var flags = string.Join("", e.PlayersReady.Select(r => r ? "1" : "0"));
			TriggerStatus = $"{e.TimeLeft} ({flags})";
			TriggerUpdated?.Invoke(this, EventArgs.Empty);
		}
	}
}
