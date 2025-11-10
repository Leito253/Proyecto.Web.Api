namespace Proyecto.Core.DTOs
{
    public class ClienteCreateDTO
    {
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
    }
}
