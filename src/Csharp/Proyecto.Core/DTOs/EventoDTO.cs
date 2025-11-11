namespace Proyecto.Core.DTOs;

public class EventoDTO
{
    public int idEvento { get; set; }
    public string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public bool Activo { get; set; }
    public int idLocal { get; set; }
}