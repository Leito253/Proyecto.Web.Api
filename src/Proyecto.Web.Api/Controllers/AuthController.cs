// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Proyecto.Modelos.Entidades;
// using Proyecto.Modelos.Servicios;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;


// namespace Proyecto.Modelos.Controllers
// {
//     [ApiController]
//     [Route("auth")]
//     public class AuthController : ControllerBase
//     {
    
//        private static List<Usuario> Usuarios = new List<Usuario>();

//         private readonly IConfiguration _config;

//         public AuthController(IConfiguration config, AuthService authService)
//         {
//             _config = config;
//             _authService = authService;
//         }
//         private readonly AuthService _authService;

//         [HttpPost("register")]
//         public IActionResult Register([FromBody] Usuario usuario)
//         {
//             var result = _authService.Register(usuario);
//             if (result)
//                 return Ok(new { message = "Usuario registrado correctamente", usuario });
//             else
//                 return BadRequest("No se pudo registrar el usuario (ya existe o datos inválidos)");
//         }

//         [HttpPost("login")]
//         public IActionResult Login([FromBody] Usuario usuario)
//         {
//             var result = _authService.Login(usuario.usuario, usuario.Contrasena);
//             if (result)
//                 return Ok("Inicio de sesión exitoso");
//             else
//                 return Unauthorized("Usuario o contraseña incorrectos");
//         }

//         [HttpPost("refresh")]
//         public IActionResult Refresh()
//         {
//             var email = User.FindFirst(ClaimTypes.Email)?.Value;
//             var usuario = Usuarios.FirstOrDefault(u => u.Email == email);
//             if (usuario == null) return Unauthorized("Token inválido.");

//             var nuevoToken = GenerateJwtToken(usuario);
//             return Ok(new { Token = nuevoToken });
//         }

//         [HttpPost("logout")]
//         [Authorize]
//         public IActionResult Logout()
//         {
//             return Ok(new { Message = "Sesión cerrada correctamente." });
//         }

//         [HttpGet("me")]
//         [Authorize]
//         public IActionResult Me()
//         {
//             var email = User.FindFirst(ClaimTypes.Email)?.Value;
//             var usuario = Usuarios.FirstOrDefault(u => u.Email == email);
//             if (usuario == null) return NotFound();

//             return Ok(new
//             {
//                 usuario.IdUsuario,
//                 usuario.usuario,
//                 usuario.Email,
//                 usuario.Rol
//             });
//         }

//         [HttpGet("roles")]
//         [Authorize(Roles = "Admin")]
//         public IActionResult Roles()
//         {
//             var roles = new List<string> { "Admin", "User", "Invitado" };
//             return Ok(roles);
//         }

//         [HttpPost("/usuarios/{IdUsuario}/roles")]
//         [Authorize(Roles = "Admin")]
//         public IActionResult AsignarRol(int IdUsuario, [FromBody] string rol)
//         {
//             var usuario = Usuarios.FirstOrDefault(u => u.IdUsuario == IdUsuario);
//             if (usuario == null) return NotFound("Usuario no encontrado.");

//             usuario.Rol = rol;
//             return Ok(new { Message = $"Rol actualizado a {rol} para {usuario.usuario}" });
//         }

//     private string GenerateJwtToken(Usuario usuario)
// {
//     var claims = new[]
//     {
//         new Claim(ClaimTypes.Email, usuario.Email),
//         new Claim(ClaimTypes.Role, usuario.Rol),
//         new Claim(ClaimTypes.Name, usuario.usuario)
//     };

//     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//     var token = new JwtSecurityToken(
//         issuer: _config["Jwt:Issuer"],
//         audience: _config["Jwt:Audience"],
//         claims: claims,
//         expires: DateTime.Now.AddHours(1),
//         signingCredentials: creds);

//     return new JwtSecurityTokenHandler().WriteToken(token);
// }

//     }

//     internal class JwtSecurityTokenHandler
//     {
//         public JwtSecurityTokenHandler()
//         {
//         }

//         internal string WriteToken(JwtSecurityToken token)
//         {
//             throw new NotImplementedException();
//         }
//     }

//     internal class JwtSecurityToken
//     {
//         private string? issuer;
//         private string? audience;
//         private Claim[] claims;
//         private DateTime expires;
//         private SigningCredentials signingCredentials;

//         public JwtSecurityToken(string? issuer, string? audience, Claim[] claims, DateTime expires, SigningCredentials signingCredentials)
//         {
//             this.issuer = issuer;
//             this.audience = audience;
//             this.claims = claims;
//             this.expires = expires;
//             this.signingCredentials = signingCredentials;
//         }
//     }

//     internal class SigningCredentials
//     {
//         private SymmetricSecurityKey key;
//         private object hmacSha256;

//         public SigningCredentials(SymmetricSecurityKey key, object hmacSha256)
//         {
//             this.key = key;
//             this.hmacSha256 = hmacSha256;
//         }
//     }

//     internal class SymmetricSecurityKey
//     {
//         private object value;

//         public SymmetricSecurityKey(object value)
//         {
//             this.value = value;
//         }
//     }

//     public class LoginRequest
//     {
//         public string Email { get; set; }
//         public string Password { get; set; }
//     }
//     }




