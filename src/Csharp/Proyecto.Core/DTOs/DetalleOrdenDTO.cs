namespace Proyecto.Core.DTOs
{
    public class DetalleOrdenDTO
    {
        public int IdDetalleOrden { get; set; }
        public int IdEntrada { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
