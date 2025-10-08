using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using Proyecto.Modelos.Repositorios;
using System.Collections.Generic;
using Proyecto.Modelos.Controllers;

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
public ActionResult<Orden?> GetById(int idOrden)
{
    var o = _repo.GetById(idOrden);
    if (o == null) return NotFound();
    return Ok(o);
}

[HttpPost]
public ActionResult Create([FromBody] Orden orden)
{
    _repo.Add(orden);
    return CreatedAtAction(nameof(GetById), new { idOrden = /* ajustar */ 0 }, orden);
}

[HttpPut("{id:int}")]
public ActionResult Update(int idOrden, [FromBody] Orden orden)
{
    _repo.Update(orden);
    return NoContent();
}


}