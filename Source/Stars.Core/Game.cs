namespace Stars.Core
{
	public class Game
	{
		public GameSettings Settings { get; set; }
		public Galaxy Galaxy { get; set; }
		public int Turn { get; set; } = 1;

		public Game()
		{
		}

		public Game(GameSettings settings, Galaxy galaxy)
		{
			Settings = settings;
			Galaxy = galaxy;
		}

		public void Update()
		{
			Turn += Settings.TimeStep;

			foreach (var planet in Galaxy.Planets)
			{
				planet.ScannerRange += 10;
			}
		}
	}

	public class GameSettings
	{
		public int TimeStep => 1;
	}
}
