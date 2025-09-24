namespace Proyecto.Modelos.Entidades;
public class Cliente
{
    public int DNI { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string contrasenia { get; set; } = string.Empty;
}