namespace Proyecto.Modelos.Entidades;

public class Evento
{
    public int idEvento { get; set; }
    public required string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public required string Lugar { get; set; }
    public required string Tipo { get; set; }
    public int LocalId { get; set; }
    public Local? Local { get; set; }
}
    
