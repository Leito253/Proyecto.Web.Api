using Proyecto.Core.Entidades;

namespace Proyecto.Modelos.Entidades;
public class Orden
{
    public int idOrden { get; set; }
    public DateTime Fecha { get; set; }
    public required string Estado { get; set; }
    public int idCliente { get; set; }
    public int NumeroOrden { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    
    /*public decimal Total => Entradas.Sum(e => e.tarifa.Precio); */
    public List<DetalleOrden> Detalles { get; set; }

}