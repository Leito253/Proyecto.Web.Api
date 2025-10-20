// using Microsoft.AspNetCore.Mvc;
// using Proyecto.Modelos.Entidades;
// using Proyecto.Modelos.Repositorios;

// namespace Proyecto.Modelos.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class LocalController : ControllerBase
//     {
//         private readonly ILocalRepository _repo;

//         public LocalController(ILocalRepository repo)
//         {
//             _repo = repo;
//         }

//         [HttpPost]
//         public IActionResult Create(Local local)
//         {
//             _repo.Add(local);
//             return CreatedAtAction(nameof(GetById), new { idLocal = local.idLocal }, local);
//         }

//         [HttpGet]
//         public ActionResult<IEnumerable<Local>> GetAll()
//         {
//             return Ok(_repo.GetAll());
//         }

//         [HttpGet("{idLocal}")]
//         public ActionResult<Local> GetById(int idLocal)
//         {
//             var local = _repo.GetById(idLocal);
//             if (local == null) return NotFound();
//             return Ok(local);
//         }

//         [HttpPut("{idLocal}")]
//         public IActionResult Update(int idLocal, Local local)
//         {
//             if (idLocal != local.idLocal) return BadRequest();
//             _repo.Update(local);
//             return NoContent();
//         }

//         [HttpDelete("{idLocal}")]
//         public IActionResult Delete(int idLocal)
//         {
//             _repo.Delete(idLocal);
//             return NoContent();
//         }
//     }
// }