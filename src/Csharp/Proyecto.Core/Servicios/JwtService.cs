using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Proyecto.Core.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto.Core.Servicios;

public class JwtService
{
    private readonly string _jwtKey;
    private readonly string _jwtIssuer;

    public JwtService(IConfiguration configuration)
    {
        _jwtKey = configuration["Jwt:Key"] ?? throw new Exception("Falta Jwt:Key en appsettings.json");
        _jwtIssuer = configuration["Jwt:Issuer"] ?? throw new Exception("Falta Jwt:Issuer en appsettings.json");
    }
    public string GenerarTokenAcceso(Usuario usuario, IEnumerable<Rol> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
            new Claim("usuario", usuario.NombreUsuario),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var rol in roles)
            claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtIssuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public Token GenerarRefreshToken(int idUsuario)
    {
        return new Token
        {
            IdUsuario = idUsuario,
            TokenRefresh = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            FechaExpiracion = DateTime.UtcNow.AddDays(7)
        };
    }
}
