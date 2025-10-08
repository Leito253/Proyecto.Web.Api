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
public ActionResult<Cliente?> GetById(int DNI)
{
    var c = _repo.GetById(DNI);
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
public ActionResult Update(int DNI, [FromBody] Cliente cliente)
{
    if (cliente.DNI != DNI) return BadRequest();
    _repo.Update(cliente);
    return NoContent();
}

[HttpDelete("{dni:int}")]
public ActionResult Delete(int DNI)
{
    _repo.Delete(DNI);
    return NoContent();
}


}