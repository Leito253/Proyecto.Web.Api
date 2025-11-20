using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Servicios.Interfaces;

namespace Proyecto.Core.Servicios
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<EventoDTO>> ObtenerEventos(string? q, string? tipo, int? localId, DateTime? desde, DateTime? hasta)
        {
            var eventos = _eventoRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(q))
                eventos = eventos.Where(e => e.Nombre.Contains(q, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(tipo))
                eventos = eventos.Where(e => e.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase));

            if (localId.HasValue)
                eventos = eventos.Where(e => e.idLocal == localId);

            if (desde.HasValue)
                eventos = eventos.Where(e => e.Fecha >= desde);

            if (hasta.HasValue)
                eventos = eventos.Where(e => e.Fecha <= hasta);

            return eventos.Select(e => new EventoDTO
            {
                idEvento = e.idEvento,
                Nombre = e.Nombre,
                Fecha = e.Fecha,
                Lugar = e.Lugar,
                Tipo = e.Tipo,
                idLocal = e.idLocal,
                Activo = e.Activo
            });
        }

        public async Task<EventoDTO?> ObtenerEventoPorId(int id)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return null;

            return new EventoDTO
            {
                idEvento = evento.idEvento,
                Nombre = evento.Nombre,
                Fecha = evento.Fecha,
                Lugar = evento.Lugar,
                Tipo = evento.Tipo,
                idLocal = evento.idLocal,
                Activo = evento.Activo
            };
        }

        public async Task<bool> CrearEvento(EventoCreateDTO dto)
        {
            var evento = new Evento
            {
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Lugar = dto.Lugar,
                Tipo = dto.Tipo,
                idLocal = dto.idLocal,
                Activo = true
            };

            _eventoRepository.Add(evento);
            return await Task.FromResult(true);
        }

        public async Task<bool> ActualizarEvento(int id, EventoUpdateDTO dto)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return false;

            evento.Nombre = dto.Nombre;
            evento.Fecha = dto.Fecha;
            evento.Lugar = dto.Lugar;
            evento.Tipo = dto.Tipo;
            evento.idLocal = dto.IdLocal;

            _eventoRepository.Update(evento);
            return await Task.FromResult(true);
        }

        public async Task<bool> ActivarEvento(int id)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return false;

            evento.Activo = true;
            _eventoRepository.Update(evento);
            return await Task.FromResult(true);
        }

        public async Task<bool> DesactivarEvento(int id)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return false;

            evento.Activo = false;
            _eventoRepository.Update(evento);
            return await Task.FromResult(true);
        }
        public async Task<bool> PublicarEvento(int id)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return false;

            evento.Publicado = true;
            _eventoRepository.Update(evento);
            return await Task.FromResult(true);
        }

        public async Task<bool> CancelarEvento(int id)
        {
            var evento = _eventoRepository.GetById(id);
            if (evento == null) return false;

            evento.Cancelado = true;
            _eventoRepository.Update(evento);
            return await Task.FromResult(true);
        }
    }
}