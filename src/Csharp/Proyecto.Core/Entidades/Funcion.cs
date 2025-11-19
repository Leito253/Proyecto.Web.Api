namespace Proyecto.Core.Entidades;

public class Funcion
{
    public int IdFuncion { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int IdEvento { get; set; }
    public int IdLocal { get; set; }
    public List<Entrada> Entradas { get; set; } = new();
}
