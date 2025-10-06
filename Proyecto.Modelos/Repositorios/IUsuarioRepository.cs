using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios;

public interface IUsuarioRepository
{
    Usuario? GetByUsuario(string user);
    Usuario? GetById(int id);
    void Add(Usuario user);
    IEnumerable<Rol> GetRoles(int IdUsuario);
    IEnumerable<Rol> GetAllRoles();
    void AssignRole(int IdUsuario, int idRol);
}
