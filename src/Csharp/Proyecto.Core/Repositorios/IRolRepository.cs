using Proyecto.Core.Entidades;
namespace Proyecto.Core.Repositorios
{
    public interface IRolRepository
    {
        IEnumerable<Rol> GetAll();
        void Add(Rol rol);
        Rol? GetById(int idRol);
        void Delete(int idRol);
    }
}