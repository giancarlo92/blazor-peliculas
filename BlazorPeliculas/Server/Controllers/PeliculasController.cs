using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos, IMapper mapper)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
            this.mapper = mapper;
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
                    Personaje = x.Personaje,
                    Id = x.Persona.Id
                }).ToList(),
                PromedioVotos = promedioVotos,
                VotoUsuario = votoUsuario
            };
        }

        [HttpGet("actualizar/{id}")]
        public async Task<ActionResult<PeliculaActualizacionDto>> PutGet(int id)
        {
            var peliculaActionResult = await Get(id);
            if (peliculaActionResult.Result is NotFoundResult) return NotFound();

            var peliculaVisualizarDto = peliculaActionResult.Value;
            var generosSeleccionadosIds = peliculaVisualizarDto.Generos.Select(x => x.Id).ToList();
            var generosNoSeleccionados = await context.Genero
                .Where(x => !generosSeleccionadosIds.Contains(x.Id))
                .ToListAsync();

            return new PeliculaActualizacionDto
            {
                Pelicula = peliculaVisualizarDto.Pelicula,
                GenerosSeleccionados = peliculaVisualizarDto.Generos,
                GenerosNoSeleccionados = generosNoSeleccionados,
                Actores = peliculaVisualizarDto.Actores
            };
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<Pelicula>>> Get([FromQuery] ParametrosBusquedaPeliculas parametrosBusqueda)
        {
            var peliculasQueryable = context.Pelicula.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parametrosBusqueda.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.ToLower().Contains(parametrosBusqueda.Titulo.ToLower()));
            } 

            if (parametrosBusqueda.EnCartelera)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.EnCartelera);
            }

            if (parametrosBusqueda.Estrenos)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Lanzamiento >= DateTime.Today);
            }

            if (parametrosBusqueda.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.GenerosPelicula.Select(y => y.GeneroId).Contains(parametrosBusqueda.GeneroId));
            }

            //Implementar Votacion


            await HttpContext.InsertarParametrosPaginacionEnRespuesta(peliculasQueryable, parametrosBusqueda.CantidadRegistros);
            return await peliculasQueryable.Paginar(parametrosBusqueda.Paginacion).ToListAsync();
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

        [HttpPut]
        public async Task<ActionResult> Put(Pelicula pelicula)
        {
            var peliculaDB = await context.Pelicula.FirstOrDefaultAsync(x => x.Id == pelicula.Id);

            if (peliculaDB == null) return NotFound();

            peliculaDB = mapper.Map(pelicula, peliculaDB);

            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var posterImagen = Convert.FromBase64String(pelicula.Poster);
                peliculaDB.Poster = await almacenadorArchivos.EditarArchivo(posterImagen, ".jpg", contenedor, peliculaDB.Poster);
            }

            await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM GeneroPelicula WHERE PeliculaId = {pelicula.Id}; DELETE FROM PeliculaActor WHERE PeliculaId = {pelicula.Id}");

            if (pelicula.PeliculasActores != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            peliculaDB.PeliculasActores = pelicula.PeliculasActores;
            peliculaDB.GenerosPelicula = pelicula.GenerosPelicula;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pelicula>> Delete(int id)
        {
            var existe = await context.Pelicula.AnyAsync(x => x.Id == id);
            if (!existe) return NotFound();
            context.Remove(new Pelicula { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
