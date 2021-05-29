using BlazorPeliculas.Client.Helpers;
using BlazorPeliculas.Client.Repositories.Pruebas;

namespace BlazorPeliculas.Client.Repositories
{
    public interface IUnitOfWork
    {
        IPrueba Prueba { get; }
        IUrlManager UrlManager { get; }
    }
}
