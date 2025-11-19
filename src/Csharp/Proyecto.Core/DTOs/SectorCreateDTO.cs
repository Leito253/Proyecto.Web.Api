using System.Text.Json.Serialization;

namespace Proyecto.Core.DTOs;

public class SectorCreateDTO
{
    public required string Nombre { get; set; }

    [JsonPropertyName("idLocal")]
    public required int idLocal { get; set; }

    public int Capacidad { get; set; }
    public decimal Precio { get; set; }
}