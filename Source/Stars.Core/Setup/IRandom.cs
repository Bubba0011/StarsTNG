namespace Stars.Core.Setup
{
	public interface IRandom
	{
		int Next(int minValue, int maxValue);

		int Norm(int minValue, int maxValue, int count);
	}
}
