namespace Proyecto.Core.Entidades;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public bool Activo { get; set; }

    // NO SE PONEN ROLES DIRECTAMENTE ACÁ
    // Los roles vienen de la tabla UsuarioRol:

    public List<Rol> Roles { get; set; } = new();
}
