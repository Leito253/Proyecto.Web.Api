namespace Proyecto.Modelos.Entidades;

public class Entrada
{
    public int idEntrada { get; set; }
    public decimal Precio { get; set; }
    public Tarifa tarifa { get; set; } = new Tarifa();
    public string QR { get; set; } = string.Empty;
    public Funcion funcion { get; set; } = default!;
    public bool Usada { get; set; }
    public bool Anulada { get; set; }
    public string Numero { get; set; } = string.Empty;
}