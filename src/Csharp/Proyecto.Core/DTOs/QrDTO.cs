namespace Proyecto.Core.DTOs;

public class QrDTO
{
    public int idQR { get; set; }
    public int IdEntrada { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}
