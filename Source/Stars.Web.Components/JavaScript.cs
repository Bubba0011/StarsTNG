using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Stars.Web.Components
{
	public static class JavaScript
	{
		public static ValueTask<T> GetElementPosition<T>(this IJSRuntime js, ElementReference element, MouseEventArgs e)
		{
			return js.InvokeAsync<T>("retrieveElementPosition", element, e);
		}

		public static ValueTask<T> GetPositionFromCorner<T>(this IJSRuntime js, ElementReference element, MouseEventArgs e)
		{
			return js.InvokeAsync<T>("retrievePosFromCorner", element, e);
		}

		public static ValueTask SetOriginPosition(this IJSRuntime js, ElementReference element, MouseEventArgs e)
		{
			return js.InvokeVoidAsync("setOrigin", element, e);
		}

		public static ValueTask Hover(this IJSRuntime js, ElementReference element, MouseEventArgs e)
		{
			return js.InvokeVoidAsync("hover", element, e);
		}

		public static ValueTask MoveViewBox(this IJSRuntime js, ElementReference element, MouseEventArgs e, double zoom)
		{
			return js.InvokeVoidAsync("moveViewBox", element, e, zoom);
		}

		public static ValueTask<T> GetScreenSize<T>(this IJSRuntime js, string elementId)
		{
			return js.InvokeAsync<T>("retrieveScreenSize", elementId);
		}
	}
}
