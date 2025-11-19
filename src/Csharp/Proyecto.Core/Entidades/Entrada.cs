namespace Proyecto.Core.Entidades;

public class Entrada
{
    public int IdEntrada { get; set; }
    public decimal Precio { get; set; }
    public string QR { get; set; } = string.Empty;
    public bool Usada { get; set; }
    public bool Anulada { get; set; }
    public DateTime? FechaUso { get; set; }
    public required string Numero { get; set; }

    public int IdDetalleOrden { get; set; }
    public int IdCliente { get; set; }
    public int IdFuncion { get; set; }
    public int IdSector { get; set; }

    public void MarcarComoUsada()
    {
        if (Anulada) throw new InvalidOperationException("La entrada está anulada.");
        if (Usada) throw new InvalidOperationException("La entrada ya fue utilizada.");

        Usada = true;
        FechaUso = DateTime.Now;
    }
}