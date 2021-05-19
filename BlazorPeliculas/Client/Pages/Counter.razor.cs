using MathNet.Numerics.Statistics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using static BlazorPeliculas.Client.Shared.MainLayout;

namespace BlazorPeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] ServiciosSingleton singleton { get; set; }
        [Inject] ServiciosTransient transient { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [CascadingParameter(Name = "Color")] protected string Color { get; set; }
        [CascadingParameter(Name = "Size")] protected string Size { get; set; }
        [CascadingParameter] protected AppState appState { get; set; }
        IJSObjectReference modulo;

        private int currentCount = 0;
        static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            var arreglo = new double[] {1,2,3,4,5 };
            var maximo = arreglo.Maximum();
            var minimo = arreglo.Minimum();

            modulo = await JS.InvokeAsync<IJSObjectReference>("import", "./js/Counter.js");
            await modulo.InvokeVoidAsync("mostrarAlerta", $"El max es {maximo} y min es {minimo}");

            currentCount++;
            singleton.Valor = currentCount;
            transient.Valor = currentCount;
            currentCountStatic = currentCount;
            await JS.InvokeVoidAsync("pruebaPuntoNetStatic");
        }

        private async Task IncrementCountJavascript()
        {
            await JS.InvokeVoidAsync("pruebaPuntoNetInstancia", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> ObtenerCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
