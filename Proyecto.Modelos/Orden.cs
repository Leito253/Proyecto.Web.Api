namespace Proyecto.Modelos;

public class Orden
{
    public int NumeroOrden { get; set; }
    public DateTime FechaCompra { get; set; }
    public Cliente Cliente { get; set; }
    public List<Entrada> Entradas { get; set; }
    public decimal Total => Entradas.Sum(e => e.tarifa.precio);
}