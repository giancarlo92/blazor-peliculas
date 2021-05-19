#pragma checksum "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\ListadoPeliculas.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "595271cb632fd4b2db7d747968f188aa8cf97895"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorPeliculas.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using BlazorPeliculas.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using BlazorPeliculas.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using BlazorPeliculas.Client.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using BlazorPeliculas.Shared.Entidades;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using BlazorPeliculas.Client.Repositories;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Services;

#line default
#line hidden
#nullable disable
    public partial class ListadoPeliculas : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Listado de Peliculas</h3>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "style", "display: flex; flex-wrap: wrap; align-items: center;");
            __Blazor.BlazorPeliculas.Client.Shared.ListadoPeliculas.TypeInference.CreateListadoGenerico_0(__builder, 3, 4, 
#nullable restore
#line 6 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\ListadoPeliculas.razor"
                              ListaPeliculas

#line default
#line hidden
#nullable disable
            , 5, (pelicula) => (__builder2) => {
                __builder2.OpenComponent<BlazorPeliculas.Client.Shared.PeliculaIndividual>(6);
                __builder2.AddAttribute(7, "Pelicula", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<BlazorPeliculas.Shared.Entidades.Pelicula>(
#nullable restore
#line 8 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\ListadoPeliculas.razor"
                                          pelicula

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(8, "EliminarPelicula", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<BlazorPeliculas.Shared.Entidades.Pelicula>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<BlazorPeliculas.Shared.Entidades.Pelicula>(this, 
#nullable restore
#line 8 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\ListadoPeliculas.razor"
                                                                      Eliminar

#line default
#line hidden
#nullable disable
                )));
                __builder2.CloseComponent();
            }
            );
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 13 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\ListadoPeliculas.razor"
       

    [Parameter] public List<Pelicula> ListaPeliculas { get; set; }

    async Task Eliminar(Pelicula pelicula)
    {
        bool confirmado = await js.Confirm($"¿Desea borrar la pelicula {pelicula.Titulo}?");
        if (confirmado)
        {
            ListaPeliculas.Remove(pelicula);
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime js { get; set; }
    }
}
namespace __Blazor.BlazorPeliculas.Client.Shared.ListadoPeliculas
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateListadoGenerico_0<Titem>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, global::System.Collections.Generic.List<Titem> __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment<Titem> __arg1)
        {
        __builder.OpenComponent<global::BlazorPeliculas.Client.Shared.ListadoGenerico<Titem>>(seq);
        __builder.AddAttribute(__seq0, "Listado", __arg0);
        __builder.AddAttribute(__seq1, "HayRegistros", __arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
