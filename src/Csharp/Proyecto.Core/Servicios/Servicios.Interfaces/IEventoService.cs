using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;

namespace Proyecto.Core.Servicios.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDTO>> ObtenerEventos(string? q, string? estado, int? localId, DateTime? desde, DateTime? hasta);
        Task<EventoDTO?> ObtenerEventoPorId(int id);
        Task<bool> CrearEvento(EventoCreateDTO dto);
        Task<bool> ActualizarEvento(int id, EventoUpdateDTO dto);
        Task<bool> PublicarEvento(int id);
        Task<bool> CancelarEvento(int id);

    }
}