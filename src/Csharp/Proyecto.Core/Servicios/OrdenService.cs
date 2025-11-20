using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;
using Proyecto.Core.Interfaces;
using Proyecto.Core.Servicios.Interfaces;
using Proyecto.Core.Repositorios;

namespace Proyecto.Core.Servicios
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IEntradaRepository _entradaRepository;
        private readonly IQrService _qrService;

        public OrdenService(IOrdenRepository ordenRepository, IEntradaRepository entradaRepository, IQrService qrService)
        {
            _ordenRepository = ordenRepository;
            _entradaRepository = entradaRepository;
            _qrService = qrService;
        }

        public async Task<IEnumerable<OrdenDTO>> ObtenerOrdenes(int? clienteId, string? estado)
        {
            var ordenes = _ordenRepository.GetAll();

            if (clienteId.HasValue)
                ordenes = ordenes.Where(o => o.idCliente == clienteId);

            if (!string.IsNullOrWhiteSpace(estado))
                ordenes = ordenes.Where(o => o.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));

            return ordenes.Select(o => new OrdenDTO
            {
                idOrden = o.idOrden,
                idCliente = o.idCliente,
                Fecha = o.Fecha,
                Total = o.Total,
                Estado = o.Estado,
                Detalles = (o.Detalles ?? new List<DetalleOrden>())
                    .Select(d => new DetalleOrdenDTO
                    {
                        IdDetalleOrden = d.IdDetalleOrden,
                        IdEvento = d.IdEvento,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
            });
        }

        public async Task<OrdenDTO?> ObtenerOrdenPorId(int id)
        {
            var orden = _ordenRepository.GetByIdWithDetalles(id);
            if (orden == null) return null;

            return new OrdenDTO
            {
                idOrden = orden.idOrden,
                idCliente = orden.idCliente,
                Fecha = orden.Fecha,
                Total = orden.Total,
                Estado = orden.Estado,
                Detalles = (orden.Detalles ?? new List<DetalleOrden>())
                    .Select(d => new DetalleOrdenDTO
                    {
                        IdDetalleOrden = d.IdDetalleOrden,
                        IdEvento = d.IdEvento,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    }).ToList()
            };
        }

        public async Task<bool> CrearOrden(OrdenCreateDTO dto)
        {
            if (dto.idFunciones.Count != dto.idTarifas.Count || dto.idTarifas.Count != dto.Cantidades.Count)
                return false;

            var orden = new Orden
            {
                idCliente = dto.idCliente,
                Fecha = DateTime.Now,
                Estado = "Creada",
                Total = 0,
                Detalles = new List<DetalleOrden>()
            };

            for (int i = 0; i < dto.idFunciones.Count; i++)
            {
                orden.Detalles.Add(new DetalleOrden
                {
                    IdEvento = dto.idFunciones[i],
                    IdTarifa = dto.idTarifas[i],
                    Cantidad = dto.Cantidades[i],
                    PrecioUnitario = 0
                });
            }

            orden.Total = orden.Detalles.Sum(d => d.PrecioUnitario * d.Cantidad);

            _ordenRepository.Add(orden);
            return true;
        }

        public async Task<bool> GenerarQrOrden(int id)
        {
            var orden = _ordenRepository.GetById(id);
            if (orden == null || !orden.Estado.Equals("Pagada", StringComparison.OrdinalIgnoreCase))
                return false;

            foreach (var detalle in orden.Detalles ?? new List<DetalleOrden>())
            {
                var entrada = _entradaRepository.GetByDetalleOrdenId(detalle.IdDetalleOrden);
                if (entrada == null) continue;

                string qrContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}";
                _qrService.GenerarQrImagen(qrContent);
            }

            return true;
        }

        public async Task<bool> PagarOrden(int id)
        {
            var result = _ordenRepository.PagarOrden(id);
            return result == "Ok";
        }

        public async Task<bool> CancelarOrden(int id)
        {
            var result = _ordenRepository.CancelarOrden(id);
            return result == "Ok";
        }
    }
}