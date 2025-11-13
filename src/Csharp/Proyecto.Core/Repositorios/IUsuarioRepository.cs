using Proyecto.Core.Entidades;

namespace Proyecto.Core.Repositorios
{
    public interface IUsuarioRepository
    {
        Usuario? GetById(int idUsuario);
        Usuario? Login(string nombreUsuario, string contrasena);
        void Add(Usuario usuario);
        IEnumerable<Rol> GetRoles(int idUsuario);
        IEnumerable<Rol> GetAllRoles();
        void AsignarRol(int idUsuario, int idRol);
    }
}
