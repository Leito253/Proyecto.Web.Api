using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.Repositorios;
namespace Proyecto.Modelos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalController : ControllerBase
{
private readonly ILocalRepository _repo;


public LocalController(ILocalRepository repo)
{
    _repo = repo;
}

[HttpGet]
public ActionResult<IEnumerable<Local>> GetAll()
    => Ok(_repo.GetAll());

[HttpGet("{id:int}")]
public ActionResult<Local?> GetById(int id)
{
    var l = _repo.GetById(id);
    if (l == null) return NotFound();
    return Ok(l);
}

[HttpPost]
public ActionResult Create([FromBody] Local local)
{
    _repo.Add(local);
    return CreatedAtAction(nameof(GetById), new { id = /* ajustar */ 0 }, local);
}

[HttpPut("{id:int}")]
public ActionResult Update(int id, [FromBody] Local local)
{
    _repo.Update(local);
    return NoContent();
}

[HttpDelete("{id:int}")]
public ActionResult Delete(int id)
{
    _repo.Delete(id);
    return NoContent();
}

}
