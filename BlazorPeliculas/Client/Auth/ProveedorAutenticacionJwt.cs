using BlazorPeliculas.Client.Helpers;
using BlazorPeliculas.Client.Repositories;
using BlazorPeliculas.Shared.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Auth
{
    public class ProveedorAutenticacionJwt : AuthenticationStateProvider, ILoginService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IRepositorio repositorio;
        private static readonly string TOKEN_KEY = "TOKEN_KEY";
        private readonly string EXPIRATION_TOKEN_KEY = "EXPIRATION_TOKEN_KEY";

        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public ProveedorAutenticacionJwt(IJSRuntime js, HttpClient httpClient, IRepositorio repositorio)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.repositorio = repositorio;
        }
        public async  override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKEN_KEY);
            if (string.IsNullOrEmpty(token))
            {
                return Anonimo;
            }

            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATION_TOKEN_KEY);
            DateTime tiempoExpiracion;
            if (DateTime.TryParse(tiempoExpiracionString, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Limpiar();
                    return Anonimo;
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    token = await RenovarToken(token);
                }
            }

            return BuildAuthenticationState(token);
        }

        private async Task<string> RenovarToken(string token)
        {
            Console.WriteLine("Renovando el token...");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var nuevoTokenResponse = await repositorio.Get<UserToken>("api/cuentas/RenovarToken");
            var nuevoToken = nuevoTokenResponse.Response;
            await js.SetInLocalStorage(TOKEN_KEY, nuevoToken.Token);
            await js.SetInLocalStorage(EXPIRATION_TOKEN_KEY, nuevoToken.Expiration.ToString());
            return nuevoToken.Token;
        }

        private bool TokenExpirado(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion <= DateTime.UtcNow;
        }

        private bool DebeRenovarToken(DateTime tiempoExpiracion)
        {
            return tiempoExpiracion.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        public async Task ManejarRenovacionToken()
        {
            var tiempoExpiracionString = await js.GetFromLocalStorage(EXPIRATION_TOKEN_KEY);
            DateTime tiempoExpiracion;

            if (DateTime.TryParse(tiempoExpiracionString, out tiempoExpiracion))
            {
                if (TokenExpirado(tiempoExpiracion))
                {
                    await Logout();
                }

                if (DebeRenovarToken(tiempoExpiracion))
                {
                    var token = await js.GetFromLocalStorage(TOKEN_KEY);
                    var nuevoToken = await RenovarToken(token);
                    var authState = BuildAuthenticationState(nuevoToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKEN_KEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATION_TOKEN_KEY, userToken.Expiration.ToString());
            var authState = BuildAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Limpiar();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }

        private async Task Limpiar()
        {
            await js.RemoveItem(TOKEN_KEY);
            await js.RemoveItem(EXPIRATION_TOKEN_KEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
