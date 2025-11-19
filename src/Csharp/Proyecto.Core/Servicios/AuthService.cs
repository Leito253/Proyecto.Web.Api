using Proyecto.Core.Entidades;

namespace Proyecto.Core.Servicios;

public class AuthService
{
    private static List<Usuario> usuarios = new();
    private static List<TokenRefreshData> refreshTokens = new();
    public bool Register(Usuario usuario)
    {
        if (usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario 
                            || u.Email == usuario.Email))
            return false;

        usuario.IdUsuario = usuarios.Count + 1;
        usuario.Activo = true;
        usuarios.Add(usuario);
        return true;
    }
    public TokenPair? Login(string nombreUsuario, string contrasena)
    {
        var usuario = usuarios.FirstOrDefault(
            u => u.NombreUsuario == nombreUsuario && 
                    u.Contrasena == contrasena && 
                    u.Activo);

        if (usuario == null)
            return null;

        return GenerarTokens(usuario.IdUsuario);
    }
    private TokenPair GenerarTokens(int idUsuario)
    {
        string access = Guid.NewGuid().ToString();
        string refresh = Guid.NewGuid().ToString();

        refreshTokens.RemoveAll(t => t.IdUsuario == idUsuario);

        refreshTokens.Add(new TokenRefreshData
        {
            IdUsuario = idUsuario,
            RefreshToken = refresh,
            Expira = DateTime.UtcNow.AddDays(7)
        });

        return new TokenPair
        {
            AccessToken = access,
            RefreshToken = refresh
        };
    }
    public TokenPair? Refresh(int idUsuario, string refreshToken)
    {
        var token = refreshTokens
            .FirstOrDefault(t => t.IdUsuario == idUsuario && t.RefreshToken == refreshToken);

        if (token == null || token.Expira < DateTime.UtcNow)
            return null;

        return GenerarTokens(idUsuario);
    }
    public void Logout(int idUsuario)
    {
        refreshTokens.RemoveAll(t => t.IdUsuario == idUsuario);
    }
    public Usuario? Me(int idUsuario)
    {
        return usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
    }
}
public class TokenPair
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}

public class TokenRefreshData
{
    public int IdUsuario { get; set; }
    public string RefreshToken { get; set; } = "";
    public DateTime Expira { get; set; }
}
