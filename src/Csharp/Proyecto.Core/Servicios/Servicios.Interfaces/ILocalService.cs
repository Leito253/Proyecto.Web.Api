using Proyecto.Core.Entidades;

namespace Servicios.Interfaces
{
    public interface ILocalService

    {
        IEnumerable<Local> GetAll();
        Local? GetById(int idLocal);
        Local Create(Local newLocal);
        Local? Update(int idLocal, Local updatedLocal);
        bool Delete(int idLocal);
        bool Exists(int idLocal);
    }
}