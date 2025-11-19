using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;
public interface IOrdenRepository
{
    IEnumerable<Orden> GetAll();
    Orden? GetById(int idOrden);
    Orden? GetByIdWithDetalles(int idOrden);
    void Add(Orden orden);
    void Update(Orden orden);
    void Pagar(int idOrden);
    void Cancelar(int idOrden);
    bool EstaPagada(int idOrden);
    string PagarOrden(int ordenId);
    string CancelarOrden(int ordenId);
}
