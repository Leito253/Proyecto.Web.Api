namespace Proyecto.Modelos.Entidades;

public class DetalleOrden
{
    public int IdDetalleOrden { get; set; }
    public int IdOrden { get; set; }
    public int IdEvento { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}