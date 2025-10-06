using Proyecto.Modelos.Entidades;

namespace Proyecto.Modelos.Servicios
{
    public class AuthService
    {
        private static List<Usuario> usuarios = new List<Usuario>();

        public bool Register(Usuario usuario)
        {
            if (usuarios.Any(u => u.User == usuario.User || u.Email == usuario.Email))
                return false;

            usuario.IdUsuario = usuarios.Count + 1;
            usuario.Activo = true;
            usuarios.Add(usuario);
            return true;
        }

        public bool Login(string user, string password)
        {
            return usuarios.Any(u => u.User == user && u.Password == password && u.Activo);
        }
    }
}
