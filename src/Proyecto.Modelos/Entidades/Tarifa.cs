
namespace Proyecto.Modelos.Entidades;
public class Tarifa
{
    public int idTarifa { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    // Claves foráneas explícitas
    public int idSector { get; set; }
    public int IdFuncion { get; set; }
    public int idEvento { get; set; }


    // Navegación
    public Sector Sector { get; set; } = default!;
    public Funcion Funcion { get; set; } = default!;

    public static implicit operator Tarifa(int v)
    {
        throw new NotImplementedException();
    }
}