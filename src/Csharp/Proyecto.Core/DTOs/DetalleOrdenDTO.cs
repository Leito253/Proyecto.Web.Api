namespace Proyecto.Core.DTOs
{
    public class DetalleOrdenDTO
    {
        public int idDetalleOrden { get; set; }
        public int idEntrada { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
