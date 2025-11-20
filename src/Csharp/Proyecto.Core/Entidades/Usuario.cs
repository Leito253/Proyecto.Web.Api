namespace Proyecto.Core.Entidades;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public List<Rol> Roles { get; set; } = new();
}
