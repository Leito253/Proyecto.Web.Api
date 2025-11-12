using Proyecto.Core.Entidades;

namespace Servicios.Interfaces
{
    public interface IOrdenService
    {
        Task<IEnumerable<Orden>> ObtenerTodas();
        Task<Orden?> ObtenerPorId(int idOrden);
        Task<bool> Crear(Orden orden);
        Task<bool> Actualizar(Orden orden);
        Task<bool> Eliminar(int idOrden);
    }

}