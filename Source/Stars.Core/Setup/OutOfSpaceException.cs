using System;

namespace Stars.Core.Setup
{
	public class OutOfSpaceException : Exception
	{
		public OutOfSpaceException(string message)
			: base(message)
		{
		}
	}
}
