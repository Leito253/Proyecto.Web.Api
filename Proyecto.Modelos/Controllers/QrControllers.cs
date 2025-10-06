// using Microsoft.AspNetCore.Mvc;
// using Proyecto.Modelos.Servicios;

// namespace Proyecto.Modelos.Controllers;

// [ApiController]
// [Route("qr")]
// public class QrController : ControllerBase
// {
//     private readonly QrService _qrService;

//     public QrController(QrService qrService)
//     {
//         _qrService = qrService;
//     }

//     // GET /entradas/{entradaId}/qr
//     [HttpGet("/entradas/{entradaId}/qr")]
//     public IActionResult GenerarQr(int entradaId)
//     {
//         var base64Qr = _qrService.GenerarQrEntrada(entradaId);
//         if (string.IsNullOrEmpty(base64Qr)) return NotFound("Entrada no encontrada");

//         return File(Convert.FromBase64String(base64Qr), "image/png");
//     }

//     // POST /qr/lote
//     [HttpPost("lote")]
//     public IActionResult GenerarQrLote([FromBody] List<int> entradaIds)
//     {
//         var qrs = entradaIds.Select(id => new { EntradaId = id, Qr = _qrService.GenerarQrEntrada(id) });
//         return Ok(qrs);
//     }

//     // POST /qr/validar
//     [HttpPost("validar")]
//     public IActionResult ValidarQr([FromBody] string qrContent)
//     {
//         var resultado = _qrService.ValidarQr(qrContent);
//         return Ok(new { Estado = resultado });
//     }
// }
