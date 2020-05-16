using Stars.Core;
using Xunit;

namespace Stars.Tests
{
	public class PositionTests
	{
		[Fact]
		public void DistanceToSelfIsZero()
		{
			var p = new Position(8, 2);

			var distanceToSelf = p.DistanceTo(p);

			Assert.Equal(0, distanceToSelf);
		}

		[Fact]
		public void DistanceIsSymmetric()
		{
			var p1 = new Position(0, 0);
			var p2 = new Position(5, 11);

			var distance1to2 = p1.DistanceTo(p2);
			var distance2to1 = p2.DistanceTo(p1);

			Assert.Equal(distance1to2, distance2to1);
		}

		[Theory]
		[InlineData(3, 4, 5.0)]
		[InlineData(4, -3, 5.0)]
		public void CalculationIsCorrect(int dx, int dy, double correctDistance)
		{
			var p1 = new Position(0, 0);
			var p2 = new Position(dx, dy);

			var distance = p1.DistanceTo(p2);

			Assert.Equal(correctDistance, distance);
		}
	}
}
