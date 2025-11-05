namespace Proyecto.Modelos.Entidades;

public class Local
{
    public int idLocal { get; set; }
    public required string Nombre { get; set; }
    public required string Direccion { get; set; }
    public int Capacidad { get; set; }
    public required string Telefono { get; set; }
}
