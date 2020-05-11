namespace Stars.Web.Components.Svg
{
	public class SvgWheelEventArgs : SvgMouseEventArgs
	{
		public double WheelDelta { get; set; }
		public double ZoomFactor { get; set; }
	}
}
