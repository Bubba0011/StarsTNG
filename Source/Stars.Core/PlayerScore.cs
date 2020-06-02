namespace Stars.Core
{
	public struct PlayerScore
	{
		public int PlayerId { get; }
		public string PlayerName { get; }
		public long Score { get; }

		public PlayerScore(int playerId, string playerName, long score)
		{
			PlayerId = playerId;
			PlayerName = playerName;
			Score = score;
		}
	}
}
