namespace Proyecto.Modelos.Entidades;

public class Funcion
{
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    public int FuncionId { get; set; }
}