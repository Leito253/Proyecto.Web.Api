using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Servicios.Interfaces;
using QRCoder;

namespace Proyecto.Core.Servicios
{
    public class QrService : IQrService
    {
        private readonly IEntradaRepository _entradaRepo;
        private readonly IQRRepository _qrRepo;

        public QrService(IEntradaRepository entradaRepo, IQRRepository qrRepo)
        {
            _entradaRepo = entradaRepo;
            _qrRepo = qrRepo;
        }

        public QrDTO GenerarQr(int idEntrada)
        {
            var entrada = _entradaRepo.GetById(idEntrada);
            if (entrada == null)
                throw new Exception("La entrada no existe");

            string contenido = $"Entrada:{entrada.IdEntrada}|Usuario:{entrada.IdUsuario}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrData);
            string imagenBase64 = qrCode.GetGraphic(10);

            var qr = new QR
            {
                IdEntrada = idEntrada,
                Codigo = imagenBase64,
                FechaCreacion = DateTime.Now
            };

            _qrRepo.Add(qr);

            return new QrDTO
            {
                idQR = qr.idQR,
                IdEntrada = qr.IdEntrada,
                Codigo = qr.Codigo,
                FechaCreacion = qr.FechaCreacion
            };
        }

        public byte[] GenerarQrEntradaImagen(string qrContent)
        {
            throw new NotImplementedException();
        }

        public QrDTO ObtenerQrPorEntrada(int idEntrada)
        {
            var qr = _qrRepo.GetByEntrada(idEntrada);
            if (qr == null) return null;

            return new QrDTO
            {
                idQR = qr.idQR,
                IdEntrada = qr.IdEntrada,
                Codigo = qr.Codigo,
                FechaCreacion = qr.FechaCreacion
            };
        }

        public object? ValidarQr(string qrContent)
        {
            throw new NotImplementedException();
        }
    }
}
