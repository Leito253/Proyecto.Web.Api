namespace Proyecto.Modelos.Entidades;

public class Evento
{
    public int idEvento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Lugar { get; set; } = string.Empty;
    public int Precio { get; set; }
    public int LocalId { get; set; }
    public Local? Local { get; set; }
}
    
