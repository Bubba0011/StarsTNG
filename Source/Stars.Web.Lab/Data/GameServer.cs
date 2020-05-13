using Stars.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stars.Web.Lab.Data
{
	public class GameServer
	{
		public  Game Game { get; }

		public EventHandler GameUpdated;

		public GameServer(Game game)
		{
			Game = game;
		}

		public void Update()
		{
			Game.Update();
			GameUpdated?.Invoke(this, EventArgs.Empty);
		}

		public PlayerView GetPlayerView(int playerId)
		{
			return new PlayerView(Game.Galaxy, playerId);
		}

		public IEnumerable<int> GetPlayerIds()
		{
			return Game.Galaxy.Players.Select(player => player.Id);
		}

		private CancellationTokenSource cancelSource;
		private Task sim;

		public bool IsRunning => sim != null;

		public Task StartAsync(TimeSpan updateInterval)
		{
			if (sim == null)
			{
				cancelSource = new CancellationTokenSource();
				sim = Task.Run(() => Run(updateInterval, cancelSource.Token));
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

		private async Task Run(TimeSpan updateInterval, CancellationToken cancel)
		{
			try
			{
				while (!cancel.IsCancellationRequested)
				{
					await Task.Delay(updateInterval, cancel);
					Update();
				}
			}
			catch (TaskCanceledException ex)
			{
			}
			catch (Exception)
			{
			}
		}
	}
}
