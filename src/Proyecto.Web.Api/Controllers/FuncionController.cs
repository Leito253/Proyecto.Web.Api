// using Microsoft.AspNetCore.Mvc;
// using Proyecto.Modelos.Entidades;
// using Proyecto.Modelos.Interfaces;
// using Proyecto.Modelos.Repositorios;

// namespace Proyecto.Modelos.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class FuncionController : ControllerBase
// {
// private readonly IFuncionRepository _repo;

// public FuncionController(IFuncionRepository repo)
// {
//     _repo = repo;
// }

// [HttpGet]
// public ActionResult<IEnumerable<Funcion>> GetAll()
//     => Ok(_repo.GetAll());

// [HttpGet("{idFuncion:int}")]
// public ActionResult<Funcion?> GetById(int IdFuncion)
// {
//     var f = _repo.GetById(IdFuncion);
//     if (f == null) return NotFound();
//     return Ok(f);
// }

// [HttpPost]
// public ActionResult Create([FromBody] Funcion funcion)
// {
//     _repo.Add(funcion);
//     return CreatedAtAction(nameof(GetById), new { IdFuncion = /* ajustar */ 0 }, funcion);
// }

// [HttpPut("{idFuncion:int}")]
// public ActionResult Update(int IdFuncion, [FromBody] Funcion funcion)
// {
//     _repo.Update(funcion);
//     return NoContent();
// }

// [HttpDelete("{idFuncion:int}")]
// public ActionResult Delete(int IdFuncion)
// {
//     _repo.Delete(IdFuncion);
//     return NoContent();
// }


// }