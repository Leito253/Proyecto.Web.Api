namespace Proyecto.Core.Entidades;

public class Sector
{
    public int idSector { get; set; }
    public required string Nombre { get; set; }
    public string Descripcion { get; set; } = String.Empty;
    public required int Capacidad { get; set; }
    public required decimal Precio { get; set; }
    public int idLocal { get; set; }
}
