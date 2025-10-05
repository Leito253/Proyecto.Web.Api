using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos;
using Proyecto.Modelos.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController : ControllerBase
    {
        private static List<Tarifa> tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, Descripcion = "General", Precio = 2500 },
            new Tarifa { idTarifa = 2, Descripcion = "Estudiante", Precio = 1500 }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(tarifas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == id);
            if (tarifa == null)
                return NotFound();
            return Ok(tarifa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tarifa nuevaTarifa)
        {
            nuevaTarifa.idTarifa = tarifas.Max(t => t.idTarifa) + 1;
            tarifas.Add(nuevaTarifa);
            return CreatedAtAction(nameof(Get), new { id = nuevaTarifa.idTarifa }, nuevaTarifa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tarifa tarifaActualizada)
        {
            var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == id);
            if (tarifa == null)
                return NotFound();

            tarifa.Descripcion = tarifaActualizada.Descripcion;
            tarifa.Precio = tarifaActualizada.Precio;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == id);
            if (tarifa == null)
                return NotFound();

            tarifas.Remove(tarifa);
            return NoContent();
        }
    }
}
