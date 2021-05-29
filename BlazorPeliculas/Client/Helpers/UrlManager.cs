using Microsoft.Extensions.Configuration;

namespace BlazorPeliculas.Client.Helpers
{
    public class UrlManager : IUrlManager
    {
        private readonly IConfiguration configuration;

        public UrlManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetUrlJsonPlaceholder => configuration["UrlJsonPlaceholder"]; 
    }
}
