using Microsoft.AspNetCore.Components;
using Stars.Core;
using Stars.Infrastructure.Data;

namespace Stars.Web.Components.Panels
{
	public class PanelBase : ComponentBase
	{
		[CascadingParameter(Name = "GameWrapper")]
		public GameWrapper GameWrapper { get; set; }

		[CascadingParameter(Name = "UIWrapper")]
		public UIWrapper UIWrapper { get; set; }

		[Parameter]
		public bool IsEnabled { get; set; } = true;

		protected IGalaxy Galaxy => GameWrapper.Client.GalaxyView;
	}
}
