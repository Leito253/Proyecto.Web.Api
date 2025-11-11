namespace Proyecto.Core.DTOs;

public class LocalCreateDTO
{
    public required string Nombre { get; set; }
    public required string Direccion { get; set; }
    public required int Capacidad { get; set; }
    public required string Telefono { get; set; }
}