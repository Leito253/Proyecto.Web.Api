using Proyecto.Modelos.Entidades;
namespace Proyecto.Core.Repositorios;

public interface ISectorRepository
{
    IEnumerable<Sector> GetAll();
    Sector? GetById(int idSector);
    void Add(Sector sector);
    void Update(Sector sector);
    void Delete(int idSector);
    void Add(int idLocal, Sector sector);
}