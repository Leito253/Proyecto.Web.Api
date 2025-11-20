namespace Proyecto.Core.Entidades;

public class Evento
{
    public int idEvento { get; set; }
    public required string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public required string Lugar { get; set; }
    public required string Tipo { get; set; }
    public int idLocal { get; set; }
    public Local? Local { get; set; }
    public bool Activo { get; set; }
    public bool Publicado { get; set; }
    public bool Cancelado { get; set; }
}