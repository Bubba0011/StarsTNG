namespace Stars.Core
{
	public struct Message
	{
		public string Body { get; set; }
		public Mood Mood { get; set; }
		public string? ObjectId { get; set; }

		public Message(string body, Mood mood = Mood.Neutral, ISpaceObject? obj = default)
		{
			Body = body;
			Mood = mood;
			ObjectId = obj?.ObjectId;
		}
	}

	public enum Mood
	{
		Neutral,
		Good,
		Bad,
	}
}
