using System;
using System.Timers;

namespace BlazorPeliculas.Client.Auth
{
    public class RenovadorToken : IDisposable
    {
        Timer timer;
        private readonly ILoginService loginService;

        public RenovadorToken(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public void Iniciar()
        {
            timer = new Timer();
            timer.Interval = 1000 * 60 * 4; //4 minutos, debe ser menor que el tiempo cercano de exppiracion
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            loginService.ManejarRenovacionToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
