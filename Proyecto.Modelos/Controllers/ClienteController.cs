using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;

namespace Proyecto.Modelos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
private readonly IClienteRepository _repo;


public ClienteController(IClienteRepository repo)
{
    _repo = repo;
}

[HttpGet]
public ActionResult<IEnumerable<Cliente>> GetAll()
{
    return Ok(_repo.GetAll());
}

// Nota: IClienteRepository usa 'dni' como clave según tu interfaz
[HttpGet("{dni:int}")]
public ActionResult<Cliente?> GetById(int dni)
{
    var c = _repo.GetById(dni);
    if (c == null) return NotFound();
    return Ok(c);
}

[HttpPost]
public ActionResult Create([FromBody] Cliente cliente)
{
    _repo.Add(cliente);
    return CreatedAtAction(nameof(GetById), new { dni = cliente.DNI }, cliente);
}

[HttpPut("{dni:int}")]
public ActionResult Update(int dni, [FromBody] Cliente cliente)
{
    if (cliente.DNI != dni) return BadRequest();
    _repo.Update(cliente);
    return NoContent();
}

[HttpDelete("{dni:int}")]
public ActionResult Delete(int dni)
{
    _repo.Delete(dni);
    return NoContent();
}


}