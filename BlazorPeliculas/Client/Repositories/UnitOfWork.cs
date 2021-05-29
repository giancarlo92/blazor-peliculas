using BlazorPeliculas.Client.Helpers;
using BlazorPeliculas.Client.Repositories.Pruebas;
using Microsoft.Extensions.Configuration;

namespace BlazorPeliculas.Client.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        IPrueba _prueba;
        public IPrueba Prueba => _prueba ?? (_prueba = new Prueba());

        IUrlManager _urlManager;
        private readonly IConfiguration configuration;

        public IUrlManager UrlManager => _urlManager ?? (_urlManager = new UrlManager(configuration));
    }
}
