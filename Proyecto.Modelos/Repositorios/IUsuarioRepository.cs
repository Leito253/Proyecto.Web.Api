using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios;

public interface IUsuarioRepository
{
    Usuario? GetByUsuario(string usuario);
    Usuario? GetById(int IdUsuario);
    void Add(Usuario usuario);
    IEnumerable<Rol> GetRoles(int IdUsuario);
    IEnumerable<Rol> GetAllRoles();
    void AsignarRoles(int IdUsuario, int idRol);
}
