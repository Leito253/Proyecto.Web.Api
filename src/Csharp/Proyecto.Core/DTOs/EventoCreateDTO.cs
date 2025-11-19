namespace Proyecto.Core.DTOs;
public class EventoCreateDTO
{
    public required string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public string Lugar { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public int idLocal { get; set; }
}
