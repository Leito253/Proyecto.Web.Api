using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Servicios;

namespace Proyecto.Modelos.Controllers;

[ApiController]
[Route("qr")]
public class QrController : ControllerBase
{
    private readonly QrService _qrService;

    public QrController(QrService qrService)
    {
        _qrService = qrService;
    }

   
    // GET /entradas/{idEntrada}/qr
    [HttpGet("/entradas/{idEntrada}/qr")]
    public IActionResult GenerarQr(int idEntrada)
    {
    var url = $"http://localhost:5001/swagger/index.html#/Proyecto.Modelos/get_";
    var qrBytes = _qrService.GenerarQrEntradaImagen(url);

    if (qrBytes == null || qrBytes.Length == 0)
        return NotFound("No se pudo generar el QR.");

    return File(qrBytes, "image/png");
    }

    /* // POST /qr/lote
    [HttpPost("lote")]
    public IActionResult GenerarQrLote([FromBody] List<int> entradaIds)
    {
        var qrs = entradaIds.Select(id => new { EntradaId = id, Qr = _qrService.GenerarQrEntrada(id) });
        return Ok(qrs);
    } */

    // POST /qr/validar
    [HttpPost("validar")]
    public IActionResult ValidarQr([FromBody] string qrContent)
    {
        var resultado = _qrService.ValidarQr(qrContent);
        return Ok(new { Estado = resultado });
    }
}
