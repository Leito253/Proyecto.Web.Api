namespace Proyecto.Core.DTOs
{
    public class UsuarioRegisterDTO
    {
        public required string NombreUsuario { get; set; }
        public required string Email { get; set; }
        public required string Contrasena { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public required string NombreUsuario { get; set; }
        public required string Contrasena { get; set; }
    }

    public class RefreshTokenDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class AsignarRolDTO
    {
        public int IdRol { get; set; }
    }
}
