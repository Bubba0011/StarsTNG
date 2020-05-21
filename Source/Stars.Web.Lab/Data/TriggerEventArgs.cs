using System;
using System.Linq;

namespace Stars.Web.Lab.Data
{
	public class TriggerEventArgs : EventArgs
	{
		public string TimeLeft { get; }
		public bool[] PlayersReady { get; }

		public TriggerEventArgs(bool[] playersReady, TimeSpan? timeLeft = null)
		{
			PlayersReady = playersReady.ToArray();
			TimeLeft = timeLeft?.ToString("mm\\:ss") ?? "-";
		}
	}
}
