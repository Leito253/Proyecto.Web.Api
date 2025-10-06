using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Servicios;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntradaController : ControllerBase
    {
private readonly QrService _qrService;

    public EntradaController(QrService qrService)
    {
        _qrService = qrService;
    }

    /* [HttpGet("{entradaId}/qr")]
    public IActionResult ObtenerQr(int entradaId)
    {
        var qrImagen = _qrService.GenerarQrEntradaImagen(entradaId);
        if (qrImagen == null) return NotFound();
        return qrImagen;
        var 
    } */



        private static List<Entrada> entradas = new List<Entrada>
        {
            new Entrada { idEntrada = 1, Numero = "A001", funcion = 1, tarifa = 1, Anulada = false },
            new Entrada { idEntrada = 2, Numero = "A002", funcion = 1, tarifa = 2, Anulada = false }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(entradas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entrada = entradas.FirstOrDefault(e => e.idEntrada == id);
            if (entrada == null)
                return NotFound();
            return Ok(entrada);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Entrada nuevaEntrada)
        {
            nuevaEntrada.idEntrada = entradas.Max(e => e.idEntrada) + 1;
            entradas.Add(nuevaEntrada);
            return CreatedAtAction(nameof(Get), new { id = nuevaEntrada.idEntrada }, nuevaEntrada);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Entrada entradaActualizada)
        {
            var entrada = entradas.FirstOrDefault(e => e.idEntrada == id);
            if (entrada == null)
                return NotFound();

            entrada.Numero = entradaActualizada.Numero;
            entrada.funcion = entradaActualizada.funcion;
            entrada.tarifa = entradaActualizada.tarifa;
            entrada.Anulada = entradaActualizada.Anulada;

            return NoContent();
        }

        [HttpPost("{id}/anular")]
        public IActionResult Anular(int id)
        {
            var entrada = entradas.FirstOrDefault(e => e.idEntrada == id);
            if (entrada == null)
                return NotFound();

            entrada.Anulada = true;
            return Ok(entrada);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entrada = entradas.FirstOrDefault(e => e.idEntrada == id);
            if (entrada == null)
                return NotFound();

            entradas.Remove(entrada);
            return NoContent();
        }
    }
}
