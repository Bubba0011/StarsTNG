using System;

namespace Stars.Infrastructure.Data
{
	public class UIWrapper
	{
		private double zoom = 1.0;
		public double Zoom
		{
			get => zoom;
			set => Set(ref zoom, value);
		}

		private bool inClickMode;
		public bool InClickMode
		{
			get => inClickMode;
			set => Set(ref inClickMode, value); 
		}

		private RenderSettings renderingSettings;
		public RenderSettings RenderSettings
		{
			get => renderingSettings;
			set => Set(ref renderingSettings, value);
		}

		public Selection Selection { get; } = new Selection();

		public event EventHandler StateChanged;

		private void OnStateChanged()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		private void Set<T>(ref T target, T value)
		{
			if (target?.Equals(value) == true) return;

			target = value;
			OnStateChanged();
		}
	}
}
