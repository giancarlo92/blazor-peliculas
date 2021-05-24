using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.Dtos;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<HomePageDto>> Get()
        {
            var limite = 6;
            var peliculasEnCartelera = await context.Pelicula
                .Where(x => x.EnCartelera)
                .Take(limite)
                .OrderByDescending(x => x.Lanzamiento)
                .ToListAsync();

            var proximosEstrenos = await context.Pelicula
                .Where(x => x.Lanzamiento > DateTime.Today)
                .Take(limite)
                .OrderBy(x => x.Lanzamiento)
                .ToListAsync();

            return new HomePageDto
            {
                PeliculasEnCartelera = peliculasEnCartelera,
                ProximosEstrenos = proximosEstrenos
            };
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaVisualizarDto>> Get(int id)
        {
            var pelicula = await context.Pelicula
                .Where(x => x.Id == id)
                .Include(x => x.GenerosPelicula).ThenInclude(x => x.Genero)
                .Include(x => x.PeliculasActores).ThenInclude(x => x.Persona)
                .FirstOrDefaultAsync();

            if (pelicula == null) NotFound();

            var promedioVotos = 4;
            var votoUsuario = 5;

            pelicula.PeliculasActores = pelicula.PeliculasActores.OrderBy(x => x.Orden).ToList();

            return new PeliculaVisualizarDto
            {
                Pelicula = pelicula,
                Generos = pelicula.GenerosPelicula.Select(x => x.Genero).ToList(),
                Actores = pelicula.PeliculasActores.Select(x => new Persona
                {
                    Nombre = x.Persona.Nombre,
                    Foto = x.Persona.Foto,
                    Personaje = x.Persona.Personaje,
                    Id = x.Persona.Id
                }).ToList(),
                PromedioVotos = promedioVotos,
                VotoUsuario = votoUsuario
            };
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var fotoPersona = Convert.FromBase64String(pelicula.Poster);
                pelicula.Poster = await almacenadorArchivos.GuardarArchivo(fotoPersona, ".jpg", contenedor);
            }

            if(pelicula.PeliculasActores != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return pelicula.Id;
        }
    }
}
