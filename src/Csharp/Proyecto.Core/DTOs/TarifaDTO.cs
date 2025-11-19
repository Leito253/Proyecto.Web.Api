namespace Proyecto.Core.DTOs;

public class TarifaDTO
{
    public int idTarifa { get; set; }
    public decimal Precio { get; set; }
    public int idFuncion { get; set; }
    public int Stock { get; set; }
    public bool Activa { get; set; }
}
