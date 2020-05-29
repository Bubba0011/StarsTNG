using Stars.Core;
using Stars.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stars.Web.Lab.Data
{
	public class GameServer
	{
		private UpdateTrigger updateTrigger;

		public Game Game { get; }

		public EventHandler GameUpdated;
		public EventHandler<TriggerEventArgs> TriggerChanged;

		public GameServer(Game game)
		{
			Game = game;
		}

		public PlayerGalaxyView GetPlayerView(GameClient client)
		{
			return new PlayerGalaxyView(Game, client.PlayerId);
		}

		public IEnumerable<int> GetPlayerIds()
		{
			return Game.Galaxy.Players.Select(player => player.Id);
		}

		public void SetPlayerReadyFlag(int playerId)
		{
			updateTrigger?.MarkPlayerAsReady(playerId);
		}

		private CancellationTokenSource cancelSource;
		private Task sim;

		public bool IsRunning => sim != null;

		public Task StartAsync(TimeSpan updateInterval)
		{
			if (sim == null)
			{
				cancelSource = new CancellationTokenSource();
				var trigger = new UpdateTrigger(Game.Galaxy.Players.Count, 2, updateInterval, 6 * updateInterval);
				sim = Task.Run(() => Run(trigger, cancelSource.Token));
			}

			return Task.CompletedTask;
		}

		public async Task StopAsync()
		{
			if (sim != null)
			{
				cancelSource.Cancel();
				await sim;

				sim = null;
			}
		}

		private async Task Run(UpdateTrigger trigger, CancellationToken cancel)
		{
			updateTrigger = trigger;

			trigger.StatusChanged += OnTriggerStatus;

			try
			{
				while (!cancel.IsCancellationRequested)
				{
					await trigger.NextTrigger(cancel);
					Update();
				}
			}
			catch (TaskCanceledException)
			{
			}
			catch (Exception)
			{
			}
			finally
			{
				trigger.StatusChanged -= OnTriggerStatus;
			}

			void OnTriggerStatus(object sender, TriggerEventArgs e)
			{
				TriggerChanged?.Invoke(this, e);
			}
		}

		private void Update()
		{
			Game.Update();
			GameUpdated?.Invoke(this, EventArgs.Empty);
		}
	}
}
