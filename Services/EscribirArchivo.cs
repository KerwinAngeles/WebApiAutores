
namespace WebApiAutores.Services
{
    public class EscribirArchivo : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string nombreArchivo = "archivo.txt";

        public EscribirArchivo(IWebHostEnvironment env)
        {
            _env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Escribir("Proceso iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Escribir("Proceso finalizado");
            return Task.CompletedTask;
        }

        private void Escribir(string mensaje)
        {
            var ruta = $@"{_env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
