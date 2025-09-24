using Proyecto.Modelos.Entidades;

namespace Servicios;

public interface ILocalService
{
    IEnumerable<Local> GetAll();
    Local? GetById(int id);
    Local Create(Local newLocal);
    Local? Update(int id, Local updatedLocal);
    bool Delete(int id);
    bool Exists(int id);
}
