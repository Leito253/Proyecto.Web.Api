namespace Proyecto.Core.Entidades;

public class Funcion
{
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaHora { get; set; }
    public List<Entrada> Entradas { get; set; } = new List<Entrada>();
    public int IdFuncion { get; set; }
    public int IdEvento { get; set; }
    public DateTime Fecha { get; set; }
    public int idLocal { get; set; }

    public static implicit operator Funcion(int v)
    {
        throw new NotImplementedException();
    }
}