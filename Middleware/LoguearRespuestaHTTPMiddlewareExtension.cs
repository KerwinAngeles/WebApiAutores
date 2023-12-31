namespace WebApiAutores.Middleware
{
    public static class LoguearRespuestaHTTPMiddlewareExtension
    {
        public static IApplicationBuilder UserLoguearRespuesta(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTPPMiddleware>();

        }
    }
}
