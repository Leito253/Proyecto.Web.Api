namespace Proyecto.Core.Entidades;

public class DetalleOrden
{
    public int IdDetalleOrden { get; set; }
    public int IdOrden { get; set; }
    public int IdEvento { get; set; }
    public int IdFuncion { get; set; }
    public int IdTarifa { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public List<Entrada> Entradas { get; set; } = new();
}
