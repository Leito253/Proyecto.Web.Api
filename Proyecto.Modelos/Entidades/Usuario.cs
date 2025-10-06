namespace Proyecto.Modelos.Entidades;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string User { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public string Rol { get; set; }
}