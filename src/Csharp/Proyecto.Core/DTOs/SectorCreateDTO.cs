namespace Proyecto.Core.DTOs;

public class SectorCreateDTO
{
    public string Nombre { get; set; } = null!;
    public int idLocal { get; set; }
    public int Capacidad { get; set; }
    public decimal Precio { get; set; }
}