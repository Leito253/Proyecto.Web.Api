using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Proyecto.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        [HttpGet("{texto}")]
        public IActionResult GenerarQr(string texto)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);

                // Renderizamos el QR en formato SVG
                var svgQrCode = new SvgQRCode(qrData);
                string svgImage = svgQrCode.GetGraphic(5);

                return Content(svgImage, "image/svg+xml");
            }
        }
    }
}
