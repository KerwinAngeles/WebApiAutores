using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAutores.Data;
using WebApiAutores.Interface;
using WebApiAutores.Services;

namespace WebApiAutores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposServiciosController : ControllerBase
    {
        private readonly ITareaService _tareaService;
        private readonly ServicioTransitorio _servicioTransitorio;
        private readonly ServicioAddScoped _servicioAddScoped;
        private readonly ServicioSinglenton _servicioSinglenton;
        
        public TiposServiciosController(ITareaService tareaService, ServicioTransitorio servicioTransitorio, ServicioAddScoped servicioAddScoped, ServicioSinglenton servicioSinglenton)
        {
            _tareaService = tareaService;
            _servicioTransitorio = servicioTransitorio;
            _servicioAddScoped = servicioAddScoped;
            _servicioSinglenton = servicioSinglenton;
        }

        [HttpGet("GUID")]
        public ActionResult Get()
        {
            var data = new
            {
                AutoresControllerTransient = _servicioTransitorio.guid,
                Servicio_Transitorio = _tareaService.ObtenerServicioTransitorio(),
                AutoresControllerScoped = _servicioAddScoped.guid,
                Servicio_AddScoped = _tareaService.ObtenerServicioScoped(),
                AutoresControllerSinglenton = _servicioSinglenton.guid,
                Servicio_Singlenton = _tareaService.ObtenerServicioSinglenton(),
            };

            return Ok(data);
        }
    }
}
