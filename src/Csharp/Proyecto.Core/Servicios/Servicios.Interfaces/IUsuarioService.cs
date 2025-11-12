using Proyecto.Core.Entidades;

namespace Servicios.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> Login(string Email, string Contrasena);
        Task<bool> Crear(Usuario usuario);
        
    }
}