using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stars.Web.Lab.Data
{
	class UpdateTrigger
	{
		private readonly object locker = new object();
		private readonly int playerLimit;
		private readonly TimeSpan limitTimer;
		private readonly TimeSpan? globalTimer;
		private bool[] playerFlags;

		public EventHandler<TriggerEventArgs> StatusChanged;

		public UpdateTrigger(int playerCount, int playerLimit, TimeSpan limitTimer, TimeSpan? globalTimer)
		{
			this.playerLimit = playerLimit;
			this.limitTimer = limitTimer;
			this.globalTimer = globalTimer;

			playerFlags = Enumerable.Range(0, playerCount)
				.Select(_ => false)
				.ToArray();
		}

		public void MarkPlayerAsReady(int playerId)
		{
			lock (locker)
			{
				if (playerId > 0 && playerId <= playerFlags.Length)
				{
					playerFlags[playerId - 1] = true;
				}
			}
		}

		static DateTime Now() => DateTime.UtcNow;

		static TimeSpan Round(TimeSpan time)
		{
			var tmp = time + TimeSpan.FromSeconds(0.5);
			return tmp - TimeSpan.FromMilliseconds(tmp.Milliseconds);
		}

		private int PlayersReady => GetPlayerFlags().Count(readyFlag => readyFlag);

		private bool[] GetPlayerFlags()
		{
			lock (locker)
			{
				return playerFlags.ToArray();
			}
		}

		public async Task NextTrigger(CancellationToken cancel)
		{
			// Reset
			lock (locker)
			{
				playerFlags = playerFlags.Select(_ => false).ToArray();
			}

			DateTime? triggerTime = Now() + globalTimer;

			void SetTriggerTime(DateTime time)
			{
				if (triggerTime == null || triggerTime > time)
				{
					triggerTime = time;
				}
			}

			CancellationTokenSource monitors = new CancellationTokenSource();
			_ = MonitorPlayers(GetPlayerFlags().Length, TimeSpan.Zero, cancel);
			_ = MonitorPlayers(playerLimit, limitTimer, cancel);

			try
			{
				while (triggerTime == null)
				{
					StatusChanged?.Invoke(this, new TriggerEventArgs(GetPlayerFlags()));
					await Task.Delay(1000, cancel);
				}

				while (Now() < triggerTime)
				{
					var timeLeft = Round(triggerTime.Value - Now());
					StatusChanged?.Invoke(this, new TriggerEventArgs(GetPlayerFlags(), timeLeft));

					await Task.Delay(1000, cancel);
				}
			}
			catch (TaskCanceledException)
			{
			}
			finally
			{
				monitors.Cancel();
			}

			async Task MonitorPlayers(int limit, TimeSpan triggerDelay, CancellationToken cancel)
			{
				while (!cancel.IsCancellationRequested)
				{
					if (PlayersReady >= limit)
					{
						SetTriggerTime(Now() + triggerDelay);
						return;
					}
					else
					{
						await Task.Delay(1000, cancel);
					}
				}
			}
		}
	}
}
