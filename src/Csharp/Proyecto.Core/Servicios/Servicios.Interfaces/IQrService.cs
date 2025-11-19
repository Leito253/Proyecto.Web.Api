using Proyecto.Core.DTOs;

namespace Proyecto.Core.Servicios.Interfaces;

public interface IQrService
{
    QrDTO GenerarQr(int idEntrada);
    byte[] GenerarQrImagen(string contenido);
    bool ValidarQr(string contenido);
    QrDTO? ObtenerQrPorEntrada(int idEntrada);
}
