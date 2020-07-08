using System.Collections.Generic;

namespace Stars.Core
{
	class OpponentPlayerView : IPlayer
	{
		private readonly Player player;

		public int Id => player.Id;
		public string? Name => player.Name;
		public IList<Message> Messages => new List<Message>();

		public OpponentPlayerView(Player player)
		{
			this.player = player;
		}

		public double? GetPlanetValue(IPlanet planet) => null;
	}
}
