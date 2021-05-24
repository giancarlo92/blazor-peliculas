using BlazorPeliculas.Shared.Entidades;
using System.Collections.Generic;

namespace BlazorPeliculas.Shared.Dtos
{
    public class HomePageDto
    {
        public List<Pelicula> PeliculasEnCartelera { get; set; }
        public List<Pelicula> ProximosEstrenos { get; set; }
    }
}
