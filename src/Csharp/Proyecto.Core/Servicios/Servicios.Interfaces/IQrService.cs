using Proyecto.Core.DTOs;

namespace Proyecto.Core.Servicios.Interfaces
{
    public interface IQrService
    {
        QrDTO GenerarQr(int idEntrada);
        QrDTO ObtenerQrPorEntrada(int idEntrada);
    }
}
