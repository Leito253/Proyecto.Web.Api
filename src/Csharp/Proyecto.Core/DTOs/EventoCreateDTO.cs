namespace Proyecto.Core.DTOs;
public class EventoCreateDTO
{
    public required string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public int idLocal { get; set; }
}