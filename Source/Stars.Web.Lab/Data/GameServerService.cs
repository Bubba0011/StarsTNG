using System;
using System.Collections.Generic;

namespace Stars.Web.Lab.Data
{
	public class GameServerService
	{
		private readonly GameStoreService store;
		private Dictionary<int, GameServer> servers = new Dictionary<int, GameServer>();

		public GameServerService(GameStoreService store)
		{
			this.store = store;
		}

		public GameServer GetServer(int gameId, bool autoCreate)
		{
			lock (servers)
			{
				if (servers.ContainsKey(gameId))
				{
					return servers[gameId];
				}
				else if (autoCreate)
				{
					var game = store.GetGame(gameId);

					if (game != null)
					{
						var server = new GameServer(game);
						servers[gameId] = server;
						return server;
					}
				}

				return null;
			}
		}

		public GameClient GetGameClient(int gameId, int playerId)
		{
			var server = GetServer(gameId, true);

			if (server == null)
			{
				return null;
			}

			if (!server.IsRunning)
			{
				server.StartAsync(TimeSpan.FromSeconds(5));
			}

			return new GameClient(server, playerId);
		}
	}
}
