using Proyecto.Core.Entidades;

namespace Servicios.Interfaces
{
    public interface IClienteService
    {
         Task<IEnumerable<Cliente>> ObtenerTodos();
    Task<Cliente?> ObtenerPorId(int id);
    Task<bool> Crear(Cliente cliente);
    Task<bool> Actualizar(Cliente cliente);
    Task<bool> Eliminar(int id);
        
    }
}