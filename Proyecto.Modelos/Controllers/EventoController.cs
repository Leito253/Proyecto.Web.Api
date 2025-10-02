using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;

namespace Proyecto.Modelos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _repo;


    public EventoController(IEventoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Evento>> GetAll()
        => Ok(_repo.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Evento?> GetById(int id)
    {
        var e = _repo.GetById(id);
        if (e == null) return NotFound();
        return Ok(e);
    }

    [HttpPost]
    public ActionResult Create([FromBody] Evento evento)
    {
        _repo.Add(evento);
        return CreatedAtAction(nameof(GetById), new { id = /* ajustar según prop */ 0 }, evento);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] Evento evento)
    {
        _repo.Update(evento);
        return NoContent();
    }

}



