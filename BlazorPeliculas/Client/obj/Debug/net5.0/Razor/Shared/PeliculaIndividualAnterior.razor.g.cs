#pragma checksum "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5c7fb5c5e2968d5533a6d7239b97b14f4c1a6196"
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
    public partial class PeliculaIndividualAnterior : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.OpenElement(1, "p");
            __builder.AddContent(2, "Titulo: ");
            __builder.AddContent(3, 
#nullable restore
#line 2 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
                 (MarkupString)Pelicula.Titulo

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n    ");
            __builder.OpenElement(5, "p");
            __builder.AddContent(6, "Lanzamiento: ");
            __builder.OpenElement(7, "b");
            __builder.AddContent(8, 
#nullable restore
#line 3 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
                        Pelicula.Lanzamiento.ToString("dd MMM yyyy")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 4 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
     if (MostrarBotones)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(9, "div");
            __builder.OpenElement(10, "button");
            __builder.AddAttribute(11, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 7 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
                              (() => EliminarPelicula.InvokeAsync(Pelicula))

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(12, "Borrar");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 9 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "C:\Users\Giancarlo\Desktop\Blazor\BlazorPeliculas\BlazorPeliculas\Client\Shared\PeliculaIndividualAnterior.razor"
       
    [Parameter] public Pelicula Pelicula { get; set; }
    [Parameter] public bool MostrarBotones { get; set; } = false;
    [Parameter] public EventCallback<Pelicula> EliminarPelicula { get; set; }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
