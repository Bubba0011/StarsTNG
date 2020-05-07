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
		public static ValueTask<T> GetScreenSize<T>(this IJSRuntime js, string elementId)
		{
			return js.InvokeAsync<T>("retrieveScreenSize", elementId);
		}
		public static ValueTask OnMouseDown(this IJSRuntime js, MouseEventArgs e)
		{
			return js.InvokeVoidAsync("onMouseDown", e);
		}
		public static ValueTask<T> OnWheel<T>(this IJSRuntime js, ElementReference element, WheelEventArgs e)
		{
			return js.InvokeAsync<T>("onWheel", element, e);
		}
		public static ValueTask BindWheelEvent(this IJSRuntime js, ElementReference element)
		{
			return js.InvokeVoidAsync("bindWheelEvent", element);
		}
		public static ValueTask ResetUI(this IJSRuntime js)
		{
			return js.InvokeVoidAsync("resetUI");
		}
	}
}
