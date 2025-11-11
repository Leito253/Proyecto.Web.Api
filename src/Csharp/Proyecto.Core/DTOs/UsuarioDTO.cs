namespace Proyecto.Core.DTOs
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Rol { get; set; }
        //public object NombreUsuario { get; set; }
    }
}
