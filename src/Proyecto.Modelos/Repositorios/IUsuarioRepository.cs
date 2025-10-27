using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Repositorios;

public interface IUsuarioRepository
{
    Usuario? GetById(int IdUsuario);
    Usuario? Login(string Email, string Contrasena);
    void Add(Usuario usuario);
    IEnumerable<Rol> GetRoles(int IdUsuario);
    IEnumerable<Rol> GetAllRoles();
    void AsignarRoles(int IdUsuario, int idRol);
}
