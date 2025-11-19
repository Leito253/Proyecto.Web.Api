using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;
public interface IQRRepository
{
    void Add(QR qr);
    QR GetByEntrada(int idEntrada);
}
