using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Interfaces;
using SkiaSharp;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios; // ✅ agregado
using System;

namespace Proyecto.Modelos.Servicios
{
    public class QrService
    {
        private readonly IEntradaRepository _entradaRepo;
        private readonly IConfiguration _config;

        public QrService(IEntradaRepository entradaRepo, IConfiguration config)
        {
            _entradaRepo = entradaRepo;
            _config = config;
        }

        // Devuelve la imagen del QR como FileResult para el navegador
        public byte[] GenerarQrEntradaImagen(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("La URL no puede estar vacía.", nameof(url));

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);

            // Convertir a PNG usando SkiaSharp
            using var bitmap = SKBitmap.Decode(qrCodeBytes);
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            return data.ToArray();
        }

        // Validación del QR
        public string ValidarQr(string qrContent)
        {
            try
            {
                var partes = qrContent.Split('|');
                if (partes.Length < 4) return "FirmaInvalida";

                int entradaId = int.Parse(partes[0]);
                int funcionId = int.Parse(partes[1]);
                int clienteId = int.Parse(partes[2]);
                string firma = partes[3];

                string firmaEsperada = _config["Qr:Key"];
                if (firma != firmaEsperada) return "FirmaInvalida";

                var entrada = _entradaRepo.GetById(entradaId);
                if (entrada == null) return "NoExiste";

                if (entrada.Usada) return "YaUsada";

               /* if (entrada.funcion != null && entrada.funcion.FechaHora < DateTime.Now)
                    return "Expirada";

                entrada.Usada = true;
                _entradaRepo.Update(entrada);*/

                return "Ok";
            }
            catch
            {
                return "FirmaInvalida";
            }
        }
    }
}
