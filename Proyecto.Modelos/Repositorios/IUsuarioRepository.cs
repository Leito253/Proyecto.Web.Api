using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios;

public interface IUsuarioRepository
{
    Usuario? GetByUsuario(string user);
    Usuario? GetById(int IdUsuario);
    void Add(Usuario user);
    IEnumerable<Rol> GetRoles(int IdUsuario);
    IEnumerable<Rol> GetAllRoles();
    void AsignarRoles(int IdUsuario, int idRol);
}
