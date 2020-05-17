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
		public EventHandler<string> TriggerChanged;

		public GameServer(Game game)
		{
			Game = game;
		}

		public PlayerView GetPlayerView(GameClient client)
		{
			return new PlayerView(Game.Galaxy, client.PlayerId);
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
				var trigger = new UpdateTrigger(updateInterval);
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

			void OnTriggerStatus(object sender, string status)
			{
				TriggerChanged?.Invoke(this, status);
			}
		}

		private void Update()
		{
			Game.Update();
			GameUpdated?.Invoke(this, EventArgs.Empty);
		}
	}

	class UpdateTrigger
	{
		private readonly TimeSpan updateInterval;
		private readonly string format;

		public EventHandler<string> StatusChanged;

		public UpdateTrigger(TimeSpan updateInterval)
		{
			this.updateInterval = updateInterval;

			format = updateInterval.TotalMinutes >= 60
				? "hh\\:mm\\:ss"
				: "mm\\:ss";
		}

		public async Task NextTrigger(CancellationToken cancel)
		{
			static DateTime Now() => DateTime.UtcNow;

			static TimeSpan Round(TimeSpan time)
			{
				var tmp = time + TimeSpan.FromSeconds(0.5);
				return tmp - TimeSpan.FromMilliseconds(tmp.Milliseconds);
			}

			var triggerTime = Now() + updateInterval;

			while (Now() < triggerTime)
			{
				var timeLeft = Round(triggerTime - Now());
				string status = timeLeft.ToString(format);
				StatusChanged?.Invoke(this, status);

				await Task.Delay(1000, cancel);
			}
		}
	}
}
