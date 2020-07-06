namespace Stars.Core
{
	public struct Message
	{
		public string Body { get; set; }
		public Mood Mood { get; set; }
		public SpaceTime TimeStamp { get; set; }
		public string? ObjectId { get; set; }

		public Message(string body, Mood mood = Mood.Neutral, ISpaceObject? obj = default)
		{
			Body = body;
			Mood = mood;
			ObjectId = obj?.ObjectId;
			TimeStamp = default;
		}
	}

	public enum Mood
	{
		Neutral,
		Good,
		Bad,
	}
}
