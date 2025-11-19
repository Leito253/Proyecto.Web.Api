using Proyecto.Core.Entidades;
namespace Proyecto.Core.Repositorios;
public interface ITokenRepository
{
    void GuardarRefreshToken(int idUsuario, string refreshToken, DateTime fechaExpiracion);
    string? GetRefreshToken(int idUsuario);
    void EliminarRefreshToken(int idUsuario);
}
