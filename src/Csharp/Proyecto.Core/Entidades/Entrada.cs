namespace Proyecto.Core.Entidades;

public class Entrada
{
    internal readonly object IdUsuario;

    public int IdEntrada { get; set; }
    public decimal Precio { get; set; }
    public string QR { get; set; } = string.Empty;
    public bool Usada { get; set; }
    public bool Anulada { get; set; }
    public required string Numero { get; set; }
    public int IdDetalleOrden { get; set; }
    public int IdSector { get; set; }
    public int IdFuncion { get; set; }
}