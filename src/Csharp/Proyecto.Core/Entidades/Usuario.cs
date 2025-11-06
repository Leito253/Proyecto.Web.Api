namespace Proyecto.Modelos.Entidades;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string usuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public required string Rol { get; set; }
}