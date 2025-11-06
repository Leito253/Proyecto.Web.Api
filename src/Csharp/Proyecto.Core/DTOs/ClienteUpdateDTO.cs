namespace Proyecto.Modelos.DTOs
{
    public class ClienteUpdateDTO
    {
        public required string DNI { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
    }
}
