
namespace Proyecto.Core.Entidades;
public class Tarifa
{
    public int idTarifa { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int idSector { get; set; }
    public int IdFuncion { get; set; }
    public int IdEvento { get; set; }
    public Sector Sector { get; set; } = default!;
    public Funcion Funcion { get; set; } = default!;

    public static implicit operator Tarifa(int v)
    {
        throw new NotImplementedException();
    }
}