/* using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;

namespace Proyecto.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _repo;

        public EventoController(IEventoRepository repo)
        {
            _repo = repo;
        }

        // GET api/eventos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventos = await _repo.GetAll();
            return Ok(eventos);
        }

        // GET api/eventos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evento = await _repo.GetById(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        // POST api/eventos
        [HttpPost]
        public async Task<IActionResult> Create(Evento evento)
        {
            await _repo.Create(evento);
            return CreatedAtAction(nameof(GetById), new { id = evento.Id }, evento);
        }

        // PUT api/eventos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Evento evento)
        {
            if (id != evento.Id) return BadRequest();

            await _repo.Update(evento);
            return NoContent();
        }

        // DELETE api/eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.Delete(id);
            return NoContent();
        }
    }
}
 */