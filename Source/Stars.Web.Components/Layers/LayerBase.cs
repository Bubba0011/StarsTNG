using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Stars.Core;
using Stars.Infrastructure.Data;

namespace Stars.Web.Components.Layers
{
	public class LayerBase : ComponentBase
	{
		[CascadingParameter(Name = "GameWrapper")]
		public GameWrapper GameWrapper { get; set; }

		[CascadingParameter(Name = "UIWrapper")]
		public UIWrapper UIWrapper { get; set; }

		[Parameter]
		public bool IsEnabled { get; set; } = true;

		[Inject]
		protected IJSRuntime JSRuntime { get; set; }

		protected IGalaxy Galaxy => GameWrapper.Client.GalaxyView;

		protected double Zoom => UIWrapper.Zoom;
	}
}
