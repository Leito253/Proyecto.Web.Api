
using Proyecto.Modelos.Entidades;

public class Orden
{
    public int NumeroOrden { get; set; }
    public DateTime FechaCompra { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    public decimal Total => Entradas.Sum(e => e.tarifa.precio);
}