namespace Proyecto.Core.DTOs;

public class EventoUpdateDTO
{
    public required string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public bool Activo { get; set; }
    public int idLocal { get; set; }
}