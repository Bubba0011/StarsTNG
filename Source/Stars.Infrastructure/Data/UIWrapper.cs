using System;

namespace Stars.Infrastructure.Data
{
	public class UIWrapper
	{
		private double zoom = 1.0;
		public double Zoom
		{
			get => zoom;
			set
			{
				zoom = value;
				OnStateChanged();
			}
		}

		public Selection Selection { get; } = new Selection();

		public event EventHandler StateChanged;

		private void OnStateChanged()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
