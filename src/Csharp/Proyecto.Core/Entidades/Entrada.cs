namespace Proyecto.Core.Entidades;

public class Entrada
{
    public int IdEntrada { get; set; }
    public int Precio { get; set; }
    public string QR { get; set; } = string.Empty;
    public bool Usada { get; set; }
    public bool Anulada { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int idCliente {get; set;}
    public int idFuncion { get; set; }
    public int idTarifa { get; set; } 
}