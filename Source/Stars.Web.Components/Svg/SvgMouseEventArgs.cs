using System;

namespace Stars.Web.Components.Svg
{
	public class SvgMouseEventArgs : EventArgs
	{
		public Vector SvgCoords { get; set; }
		public Vector ScreenCoords { get; set; }
	}
}
