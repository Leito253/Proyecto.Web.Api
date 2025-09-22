namespace Proyecto.Modelos.Entidades
{
    public class Evento
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int LocalId { get; set; }
        public required string Descripcion { get; set; }
    }
}
