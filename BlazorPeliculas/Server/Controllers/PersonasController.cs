using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "personas";
        public PersonasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this._almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Persona persona)
        {
            if (!string.IsNullOrWhiteSpace(persona.Foto))
            {
                var fotoPersona = Convert.FromBase64String(persona.Foto);
                persona.Foto = await _almacenadorArchivos.GuardarArchivo(fotoPersona, ".jpg", contenedor);
            }
            context.Add(persona);
            await context.SaveChangesAsync();
            return persona.Id;
        }
    }
}
