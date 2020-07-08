using System.Collections.Generic;

namespace Stars.Core
{
	public class Player : IEntity
	{
		private readonly object playerLock = new object();

		public int Id { get; set; }
		public string? Name { get; set; }
		public Race Race { get; set; } = new Race();
		public List<Message> Messages { get; set; } = new List<Message>();
		
		internal void AddMessage(Message message)
		{
			lock (playerLock)
			{
				Messages.Add(message);
			}
		}

		internal void ClearMessages()
		{
			lock (playerLock)
			{
				Messages.Clear();
			}
		}

		internal IList<Message> GetMessages()
		{
			lock (playerLock)
			{
				return Messages.ToArray();
			}
		}
	}
}
