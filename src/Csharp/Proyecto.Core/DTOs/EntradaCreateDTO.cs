namespace Proyecto.Core.DTOs;

public class EntradaCreateDTO
{
    public decimal Precio { get; set; }
    public required string Numero { get; set; }
    public int IdDetalleOrden { get; set; }
    public int IdSector { get; set; }
    public int IdFuncion { get; set; }
}
