using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;

namespace Proyecto.Modelos.Controllers
{
    [ApiController]
    [Route("api")]
    public class SectorController : ControllerBase
    {
        private readonly ISectorRepository _repo;

        public SectorController(ISectorRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("local/{idLocal}/sectores")]
        public IActionResult Create(int idLocal, Sector sector)
        {
            _repo.Add(idLocal, sector);
            return CreatedAtAction(nameof(GetByLocal), new { id  = idLocal }, sector);
        }

        [HttpGet("local/{idLocal}/sector")]
        public ActionResult<IEnumerable<Sector>> GetByLocal(int idLocal)
        {
            return Ok(_repo.GetByLocal(idLocal));
        }

        [HttpPut("sectores/{idSector}")]
        public IActionResult Update(int idSector, Sector sector)
        {
            if (idSector != sector.idSector) return BadRequest();
            _repo.Update(sector);
            return NoContent();
        }

        [HttpDelete("sectores/{idSector}")]
        public IActionResult Delete(int idSector)
        {
            _repo.Delete(idSector);
            return NoContent();
        }
    }
}