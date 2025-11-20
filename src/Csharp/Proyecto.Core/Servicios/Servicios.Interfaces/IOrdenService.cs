using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Servicios.Interfaces
{
    public interface IOrdenService
    {
        Task<IEnumerable<OrdenDTO>> ObtenerOrdenes(int? clienteId, string? estado);
        Task<OrdenDTO?> ObtenerOrdenPorId(int id);
        Task<bool> CrearOrden(OrdenCreateDTO dto);
        Task<bool> GenerarQrOrden(int id);
        Task<bool> PagarOrden(int id);
        Task<bool> CancelarOrden(int id);
    }
}