namespace Proyecto.Core.DTOs;

public class EntradaDTO
{
    public int idEntrada { get; set; }
    public int Precio { get; set; }
    public required string Numero { get; set; }
    public bool Usada { get; set; }
    public bool Anulada { get; set; }
    public string QR { get; set; } = String.Empty;
}