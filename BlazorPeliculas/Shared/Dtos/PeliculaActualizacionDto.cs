using BlazorPeliculas.Shared.Entidades;
using System.Collections.Generic;

namespace BlazorPeliculas.Shared.Dtos
{
    public class PeliculaActualizacionDto
    {
        public Pelicula Pelicula { get; set; }
        public List<Persona> Actores { get; set; }
        public List<Genero> GenerosSeleccionados { get; set; }
        public List<Genero> GenerosNoSeleccionados { get; set; }
    }
}
