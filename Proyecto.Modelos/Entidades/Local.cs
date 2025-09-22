namespace Proyecto.Modelos.Entidades
{
    public class Local
    {
        public int Id_local { get; set; } // Auto-increment en la base de datos
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public int Capacidad { get; set; }
        public required string Telefono { get; set; }
    }
}
