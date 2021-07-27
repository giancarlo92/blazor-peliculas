using BlazorPeliculas.Shared.Dtos;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken token);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
