using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;

public interface IEntradaRepository
{
    IEnumerable<Entrada> GetAll();
    Entrada? GetById(int idEntrada);
    void Add(Entrada entrada);
    void Update(Entrada entrada);
    void Anular(int idEntrada);
    Entrada? GetByDetalleOrdenId(int IdDetalleOrden);
}
