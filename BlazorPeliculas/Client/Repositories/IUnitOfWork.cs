using BlazorPeliculas.Client.Repositories.Pruebas;

namespace BlazorPeliculas.Client.Repositories
{
    public interface IUnitOfWork
    {
        IPrueba Prueba { get; }
    }
}
