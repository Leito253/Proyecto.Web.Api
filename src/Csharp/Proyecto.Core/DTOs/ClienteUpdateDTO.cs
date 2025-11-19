namespace Proyecto.Core.DTOs;

public class ClienteUpdateDTO
{
    public int DNI { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Email { get; set; }
    public string Telefono { get; set; } = string.Empty;
}
