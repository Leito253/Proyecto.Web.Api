/* using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrController : ControllerBase
    {
        [HttpGet("{texto}")]
        public IActionResult GenerarQr(string texto)
        {
            // Generador del QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);

            // Genera el PNG en bytes (no necesita System.Drawing)
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPng = qrCode.GetGraphic(20);

            // Devuelve la imagen como PNG
            return File(qrCodeAsPng, "image/png");
        }
    }
}
 */