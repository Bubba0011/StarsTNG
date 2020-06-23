namespace Stars.Core
{
	public struct Message
	{
		public string Body { get; set; }
		public Mood Mood { get; set; }

		public Message(string body, Mood mood = Mood.Neutral)
		{
			Body = body;
			Mood = mood;
		}
	}

	public enum Mood
	{
		Neutral,
		Good,
		Bad,
	}
}
