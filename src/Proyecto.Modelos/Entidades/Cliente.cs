namespace Proyecto.Modelos.Entidades;
public class Cliente
{
    public int idCliente { get; set; }
    public int DNI { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Email { get; set; }
    public required string Telefono { get; set; }
}