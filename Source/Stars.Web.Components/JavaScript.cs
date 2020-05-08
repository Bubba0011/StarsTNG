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
			return js.InvokeAsync<T>("retrieveElementPosition", e);
		}
		public static ValueTask<T> GetPositionFromCorner<T>(this IJSRuntime js, ElementReference element, MouseEventArgs e)
		{
			return js.InvokeAsync<T>("retrievePosFromCorner", e);
		}
		public static ValueTask<T> GetScreenSize<T>(this IJSRuntime js, string elementId)
		{
			return js.InvokeAsync<T>("retrieveScreenSize", elementId);
		}
		public static ValueTask OnMouseDown(this IJSRuntime js, MouseEventArgs e)
		{
			return js.InvokeVoidAsync("onMouseDown", e);
		}
		public static ValueTask BindCallbackMethod<T>(this IJSRuntime js, DotNetObjectReference<T> obj, string name) where T : class
		{
			return js.InvokeVoidAsync("bindCallbackMethod", obj, name);
		}
		public static ValueTask InitializeJS(this IJSRuntime js, ElementReference element)
		{
			js.InvokeVoidAsync("resetUI");
			return js.InvokeVoidAsync("bindWheelEvent", element);
		}
	}
}
