namespace Proyecto.Modelos.DTOs
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }
        public required string usuario { get; set; }
        public required string Rol { get; set; }
    }
}
