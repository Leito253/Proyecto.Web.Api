using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios;
public interface IUsuarioRepository
{
    Usuario? GetById(int idUsuario);
    Usuario? GetByNombreUsuario(string nombreUsuario);
    void Add(Usuario usuario);

    Usuario? Login(string nombreUsuario, string contrasena);

    IEnumerable<Rol> GetAllRoles();
    IEnumerable<Rol> GetRoles(int idUsuario);
    void AsignarRol(int idUsuario, int idRol);
}
