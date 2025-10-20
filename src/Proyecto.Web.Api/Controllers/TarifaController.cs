// using Microsoft.AspNetCore.Mvc;
// using Proyecto.Modelos;
// using Proyecto.Modelos.Entidades;
// using System.Collections.Generic;
// using System.Linq;

// namespace Proyecto.Web.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class TarifaController : ControllerBase
//     {
//         private static List<Tarifa> tarifas = new List<Tarifa>
//         {
//             new Tarifa { idTarifa = 1, Descripcion = "General", Precio = 2500 },
//             new Tarifa { idTarifa = 2, Descripcion = "Estudiante", Precio = 1500 }
//         };

//         [HttpGet]
//         public IActionResult Get()
//         {
//             return Ok(tarifas);
//         }

//         [HttpGet("{idTarifa}")]
//         public IActionResult Get(int idTarifa)
//         {
//             var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == idTarifa);
//             if (tarifa == null)
//                 return NotFound();
//             return Ok(tarifa);
//         }

//         [HttpPost]
//         public IActionResult Post([FromBody] Tarifa nuevaTarifa)
//         {
//             nuevaTarifa.idTarifa = tarifas.Max(t => t.idTarifa) + 1;
//             tarifas.Add(nuevaTarifa);
//             return CreatedAtAction(nameof(Get), new { id = nuevaTarifa.idTarifa }, nuevaTarifa);
//         }

//         [HttpPut("{idTarifa}")]
//         public IActionResult Put(int idTarifa, [FromBody] Tarifa tarifaActualizada)
//         {
//             var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == idTarifa);
//             if (tarifa == null)
//                 return NotFound();

//             tarifa.Descripcion = tarifaActualizada.Descripcion;
//             tarifa.Precio = tarifaActualizada.Precio;

//             return NoContent();
//         }

//         [HttpDelete("{idTarifa}")]
//         public IActionResult Delete(int idTarifa)
//         {
//             var tarifa = tarifas.FirstOrDefault(t => t.idTarifa == idTarifa);
//             if (tarifa == null)
//                 return NotFound();

//             tarifas.Remove(tarifa);
//             return NoContent();
//         }
//     }
// }
