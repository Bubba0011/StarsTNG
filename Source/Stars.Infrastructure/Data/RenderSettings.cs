namespace Stars.Infrastructure.Data
{
	public struct RenderSettings
	{
		public UiDisplayMode MainMode { get; set; }

		public RenderSettings Toggle()
		{
			var result = this;

			result.MainMode = MainMode switch
			{
				UiDisplayMode.Default => UiDisplayMode.PlanetValue,
				UiDisplayMode.PlanetValue => UiDisplayMode.Diplo,
				_ => UiDisplayMode.Default,
			};

			return result;
		}
	}
}
