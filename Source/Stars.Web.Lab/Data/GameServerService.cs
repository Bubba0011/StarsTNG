using System;

namespace Stars.Web.Lab.Data
{
	public class GameServerService
	{
		private GameServer server;

		public GameServerService(GameStoreService store)
		{
			var game = store.GetGame();
			server = new GameServer(game);
		}

		public GameServer GetServer()
		{
			return server;
		}

		public GameClient GetGameClient(int playerId)
		{
			if (!server.IsRunning)
			{
				server.StartAsync(TimeSpan.FromSeconds(5));
			}

			return new GameClient(server, playerId);
		}
	}
}
