namespace Stars.Infrastructure.Data
{
	public struct RenderSettings
	{
		public UiDisplayMode MainMode { get; set; }

		public RenderSettings Toggle()
		{
			var result = this;

			result.MainMode = MainMode == UiDisplayMode.Default
				? UiDisplayMode.PlanetValue
				: UiDisplayMode.Default;

			return result;
		}
	}
}
