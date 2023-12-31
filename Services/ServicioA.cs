using WebApiAutores.Interface;

namespace WebApiAutores.Services
{
    public class ServicioA : ITareaService
    {
        private readonly ServicioTransitorio _servicioTransitorio;
        private readonly ServicioAddScoped _servicioAddScoped;
        private readonly ServicioSinglenton _servicioSinglenton;


        public ServicioA(ServicioTransitorio servicioTransitorio, ServicioAddScoped servicioAddScoped, ServicioSinglenton servicioSinglenton) 
        {
            _servicioTransitorio = servicioTransitorio;
            _servicioAddScoped = servicioAddScoped;
            _servicioSinglenton = servicioSinglenton;
        }

        public Guid ObtenerServicioTransitorio() { return _servicioTransitorio.guid; }
        public Guid ObtenerServicioScoped() { return _servicioAddScoped.guid; }
        public Guid ObtenerServicioSinglenton() { return _servicioSinglenton.guid; }

        public void RealizarTarea()
        {
            Console.WriteLine("Probando");
        }
    }
}
