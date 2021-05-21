using BlazorPeliculas.Client.Repositories.Pruebas;

namespace BlazorPeliculas.Client.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        IPrueba _prueba;
        public IPrueba Prueba => _prueba ?? (_prueba = new Prueba());
    }
}
