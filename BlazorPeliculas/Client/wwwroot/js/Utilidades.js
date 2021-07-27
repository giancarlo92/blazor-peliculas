function pruebaPuntoNetStatic() {
    DotNet.invokeMethodAsync("BlazorPeliculas.Client", "ObtenerCurrentCount")
        .then(resultado => {
            console.log("conteo desde javascript" + resultado);
        });
}

//EJECUTA METODOS DE C#
function pruebaPuntoNetInstancia(dotnetHelper) {
    dotnetHelper.invokeMethodAsync("IncrementCount");
}

const timeExpiration = 1000 * 60 * 3;

function timerInactivo(dotnetHelper) {
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, timeExpiration);
    }

    function logout() {
        //dotnetHelper.invokeMethodAsync("Logout");
    }
}
