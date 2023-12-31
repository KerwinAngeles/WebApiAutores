namespace WebApiAutores.Middleware
{
    public class LoguearRespuestaHTPPMiddleware
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<LoguearRespuestaHTPPMiddleware> _logger;

        public LoguearRespuestaHTPPMiddleware(RequestDelegate siguiente, ILogger<LoguearRespuestaHTPPMiddleware> logger) 
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            // usar un memory stream para guardar en memoria la respuesta HTTP, 
            // porque esta se encuentra en un buffer.

            using (var ms = new MemoryStream())
            {
                var cuerpoRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await _siguiente(contexto); // le permitimo a la tuberia de proceso continuar.
                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd(); // guardara lo que sea que le vayamos a responder al cliente.
                                                                     // guardar el stream en la pocision inicial para enviar la respuesta correcta al usuario.
                ms.Seek(0, SeekOrigin.Begin);
                await ms.CopyToAsync(cuerpoRespuesta);
                contexto.Response.Body = cuerpoRespuesta;

                _logger.LogInformation(respuesta);
            }
        }
    }
}
