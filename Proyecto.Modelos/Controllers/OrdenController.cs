using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.Repositorios;

namespace Proyecto.Modelos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenController : ControllerBase
{
private readonly IOrdenRepository _repo;


public OrdenController(IOrdenRepository repo)
{
    _repo = repo;
}

[HttpGet]
public ActionResult<IEnumerable<Orden>> GetAll()
    => Ok(_repo.GetAll());

[HttpGet("{id:int}")]
public ActionResult<Orden?> GetById(int id)
{
    var o = _repo.GetById(id);
    if (o == null) return NotFound();
    return Ok(o);
}

[HttpPost]
public ActionResult Create([FromBody] Orden orden)
{
    _repo.Add(orden);
    return CreatedAtAction(nameof(GetById), new { id = /* ajustar */ 0 }, orden);
}

[HttpPut("{id:int}")]
public ActionResult Update(int id, [FromBody] Orden orden)
{
    _repo.Update(orden);
    return NoContent();
}


}
