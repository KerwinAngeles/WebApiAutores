using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{
    public class FiltroExeception : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroExeception> _logger;

        public FiltroExeception(ILogger<FiltroExeception> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
