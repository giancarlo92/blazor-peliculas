using BlazorPeliculas.Shared.Entidades;
using System.Collections.Generic;

namespace BlazorPeliculas.Shared.Dtos
{
    public class PeliculaVisualizarDto
    {
        public Pelicula Pelicula { get; set; }
        public List<Genero> Generos { get; set; }
        public List<Persona> Actores  { get; set; }
        public int VotoUsuario  { get; set; }
        public double PromedioVotos  { get; set; }
    }
}
