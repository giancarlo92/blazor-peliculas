using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneroPelicula>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.PeliculaId, x.PersonaId });

            //var personas = new List<Persona>();
            //for (int i = 5; i < 1000; i++)
            //{
            //    personas.Add(new Persona { Id = i, Nombre = $"Persona {i}", FechaNacimiento = DateTime.Now });
            //}
            //modelBuilder.Entity<Persona>().HasData(personas);

            var roleAdmin = new IdentityRole { Id = "0e72f52f-6fee-452f-9de2-223e25aa5e19", Name = "admin", NormalizedName = "admin" };
            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GeneroPelicula> GeneroPelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PeliculaActor> PeliculaActor { get; set; }
        public DbSet<VotoPelicula> VotoPelicula { get; set; }
    }
}
