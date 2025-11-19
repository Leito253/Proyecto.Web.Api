namespace Proyecto.Core.DTOs;

public class ClienteDTO
{
    public int idCliente { get; set; }
    public int DNI { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = string.Empty;
}
