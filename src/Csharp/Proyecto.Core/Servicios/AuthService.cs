using Proyecto.Core.Entidades;

namespace Proyecto.Core.Servicios;

public class AuthService
{
    private static List<Usuario> usuarios = new List<Usuario>();

    public bool Register(Usuario usuario)
    {
        if (usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario || u.Email == usuario.Email))
            return false;

        usuario.IdUsuario = usuarios.Count + 1;
        usuario.Activo = true;
        usuarios.Add(usuario);
        return true;
    }

    public bool Login(string usuario, string Contrasena)
    {
        return usuarios.Any(u => u.NombreUsuario == usuario && u.Contrasena == Contrasena && u.Activo);
    }
}
