namespace Proyecto.Modelos;
using Proyecto.Modelos;
public class Entrada
{
    public Tarifa tarifa { get; set; } = new Tarifa();
    public string QR { get; set; } = string.Empty;
    public Funcion funcion { get; set; } = default!;
    public bool Usada { get; set; }
}