using Proyecto.Modelos.Entidades;
namespace Proyecto.Core.Repositorios
{
    public interface ILocalRepository
    {
        IEnumerable<Local> GetAll();
        Local? GetById(int idLocal);
        int Add(Local local);
        void Update(Local local);
        bool Delete(int idLocal);
        bool TieneFuncionesVigentes(int idLocal);
    }
}
