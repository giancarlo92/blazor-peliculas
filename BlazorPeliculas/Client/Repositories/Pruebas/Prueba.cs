using System.Collections.Generic;

namespace BlazorPeliculas.Client.Repositories.Pruebas
{
    public class Prueba : IPrueba
    {
        public List<string> ObtenerTexto()
        {
            return new List<string> { "cadena 1", "cadena 2", "cadena 3" };
        }
    }
}
