namespace Proyecto.Core.DTOs;

public class QrValidacionDTO
{
    public string Estado { get; set; } = "";  // Ok, YaUsada, Expirada, FirmaInvalida, NoExiste
    public string Mensaje { get; set; } = "";
    public EntradaDTO? Entrada { get; set; }
}

