using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;

public interface ISectorRepository
{
    IEnumerable<Sector> GetAll();
    IEnumerable<Sector> GetByLocal(int idLocal);
    Sector? GetById(int idSector);

    int Add(Sector sector);
    void Update(Sector sector);
    void Delete(int idSector);
}
