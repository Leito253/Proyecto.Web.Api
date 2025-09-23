namespace Proyecto.Modelos.Entidades
{
<<<<<<< HEAD
    public class Evento
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public int LocalId { get; set; }
        public required string Descripcion { get; set; }
    }
=======
    public int idEvento { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Lugar { get; set; } = string.Empty;
    public int Precio { get; set; }

    public int LocalId { get; set; }
    public Local? Local { get; set; }
>>>>>>> 9d3856547fd19ee81e4ceccf56a6d37739909b80
}
