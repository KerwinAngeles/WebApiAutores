
namespace WebApiAutores.Interface
{
    public interface ITareaService
    {
        Guid ObtenerServicioScoped();
        Guid ObtenerServicioSinglenton();
        Guid ObtenerServicioTransitorio();
        void RealizarTarea();
    }
}
