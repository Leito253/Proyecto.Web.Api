using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Servicios;

namespace Proyecto.Modelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController()
        {
            _authService = new AuthService();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario usuario)
        {
            var result = _authService.Register(usuario);
            if (result)
                return Ok(new { message = "Usuario registrado correctamente", usuario });
            else
                return BadRequest("No se pudo registrar el usuario (ya existe o datos inválidos)");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            var result = _authService.Login(usuario.User, usuario.Password);
            if (result)
                return Ok("Inicio de sesión exitoso");
            else
                return Unauthorized("Usuario o contraseña incorrectos");
        }
    }
}
