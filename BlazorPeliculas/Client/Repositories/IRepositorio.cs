using BlazorPeliculas.Shared.Entidades;
using System.Collections.Generic;

namespace BlazorPeliculas.Client.Repositories
{
    public interface IRepositorio
    {
        List<Pelicula> ObtenerPeliculas();
    }
}
