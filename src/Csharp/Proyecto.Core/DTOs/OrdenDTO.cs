namespace Proyecto.Core.DTOs;

public class OrdenDTO
{
    public int idOrden { get; set; }
    public int idCliente { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public List<DetalleOrdenDTO> Detalles { get; set; } = new();
}

