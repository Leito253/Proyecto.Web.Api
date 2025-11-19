using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;
using Proyecto.Core.Repositorios;
using Proyecto.Core.Servicios.Interfaces;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Proyecto.Core.Servicios;
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

        if (entrada.Anulada)
            throw new Exception("La entrada está anulada");

        string contenido = $"{entrada.IdEntrada}|{entrada.IdCliente}|{entrada.IdFuncion}";

        var qrBytes = GenerarQrImagen(contenido);
        string base64 = Convert.ToBase64String(qrBytes);

        var qr = new QR
        {
            IdEntrada = idEntrada,
            Codigo = base64,
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

    public byte[] GenerarQrImagen(string contenido)
    {
        QRCodeGenerator gen = new QRCodeGenerator();
        var data = gen.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
        var png = new PngByteQRCode(data);
        return png.GetGraphic(20);
    }

    public QrDTO? ObtenerQrPorEntrada(int idEntrada)
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

    public bool ValidarQr(string contenido)
    {
        var partes = contenido.Split('|');
        if (partes.Length < 3) return false;

        int idEntrada = int.Parse(partes[0]);

        var entrada = _entradaRepo.GetById(idEntrada);
        if (entrada == null) return false;
        if (entrada.Usada) return false;
        if (entrada.Anulada) return false;

        return true;
    }
}
