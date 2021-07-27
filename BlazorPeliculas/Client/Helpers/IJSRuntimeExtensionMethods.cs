using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "primer parametro");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }

        public static async ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
        {
            return await js.InvokeAsync<object>("localStorage.setItem", key, content);
        }

        public static async ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
        {
            return await js.InvokeAsync<string>("localStorage.getItem", key);
        }

        public static async ValueTask<object> RemoveItem(this IJSRuntime js, string key)
        {
            return await js.InvokeAsync<object>("localStorage.removeItem", key);
        }

        public static async ValueTask InicializarTimerInactivo<T>(this IJSRuntime js, DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("timerInactivo", dotNetObjectReference);
        }
    }
}
