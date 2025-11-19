using System.Security.Cryptography;
using System.Text;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entidades;
using Proyecto.Core.Repositorios;
using Microsoft.Extensions.Configuration;

namespace Proyecto.Core.Servicios;

public interface IQrService
{
    byte[] GenerarQrImagen(string contenido);
    string GenerarContenidoQr(Entrada entrada, string secretKey);
    QrValidacionDTO ValidarQr(string contenido);
}

public class QrService : IQrService
{
    private readonly IEntradaRepository _entradaRepo;
    private readonly IQRRepository _qrRepo;
    private readonly IConfiguration _config;

    public QrService(IEntradaRepository entradaRepo, IQRRepository qrRepo, IConfiguration config)
    {
        _entradaRepo = entradaRepo;
        _qrRepo = qrRepo;
        _config = config;
    }
    public byte[] GenerarQrImagen(string contenido)
    {
        using var generator = new QRCoder.QRCodeGenerator();
        var data = generator.CreateQrCode(contenido, QRCoder.QRCodeGenerator.ECCLevel.Q);
        var qr = new QRCoder.PngByteQRCode(data);
        return qr.GetGraphic(20);
    }
    public string GenerarContenidoQr(Entrada entrada, string secretKey)
    {
        string baseContent = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{DateTime.UtcNow:O}";

        string firma = GenerarFirma(baseContent, secretKey);

        return $"{baseContent}|{firma}";
    }

    private string GenerarFirma(string texto, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(texto));
        return Convert.ToBase64String(hash);
    }

    private bool VerificarFirma(string texto, string firma, string key)
    {
        string firmaCorrecta = GenerarFirma(texto, key);
        return firmaCorrecta == firma;
    }
    public QrValidacionDTO ValidarQr(string contenido)
    {
        try
        {
            var partes = contenido.Split('|');
            if (partes.Length != 4)
            {
                return new QrValidacionDTO
                {
                    Estado = "FormatoInvalido",
                    Mensaje = "El QR tiene un formato incorrecto."
                };
            }

            int idEntrada = int.Parse(partes[0]);
            int idFuncion = int.Parse(partes[1]);
            DateTime fechaGeneracion = DateTime.Parse(partes[2]);
            string firma = partes[3];

            string baseContent = $"{idEntrada}|{idFuncion}|{fechaGeneracion:O}";
            string secretKey = _config["Qr:Key"]!;

            if (!VerificarFirma(baseContent, firma, secretKey))
                return new QrValidacionDTO
                {
                    Estado = "FirmaInvalida",
                    Mensaje = "El QR no es válido o fue alterado."
                };

            if (DateTime.UtcNow > fechaGeneracion.AddMinutes(30))
                return new QrValidacionDTO
                {
                    Estado = "Expirada",
                    Mensaje = "El QR está expirado."
                };

            var entrada = _entradaRepo.GetById(idEntrada);
            if (entrada == null)
                return new QrValidacionDTO
                {
                    Estado = "NoExiste",
                    Mensaje = "La entrada asociada no existe."
                };

            if (entrada.Usada)
                return new QrValidacionDTO
                {
                    Estado = "YaUsada",
                    Mensaje = "La entrada ya fue utilizada.",
                    Entrada = new EntradaDTO
                    {
                        idEntrada = entrada.IdEntrada,
                        IdFuncion = entrada.IdFuncion,
                        Numero = entrada.Numero,
                        Precio = entrada.Precio,
                        Usada = entrada.Usada,
                        Anulada = entrada.Anulada,
                        QR = entrada.QR,
                        IdDetalleOrden = entrada.IdDetalleOrden,
                        IdSector = entrada.IdSector
                    }
                };

            entrada.Usada = true;
            _entradaRepo.Update(entrada);

            return new QrValidacionDTO
            {
                Estado = "Ok",
                Mensaje = "QR válido. Acceso permitido.",
                Entrada = new EntradaDTO
                {
                    idEntrada = entrada.IdEntrada,
                    IdFuncion = entrada.IdFuncion,
                    Numero = entrada.Numero,
                    Precio = entrada.Precio,
                    Usada = entrada.Usada,
                    Anulada = entrada.Anulada,
                    QR = entrada.QR,
                    IdDetalleOrden = entrada.IdDetalleOrden,
                    IdSector = entrada.IdSector
                }
            };
        }
        catch
        {
            return new QrValidacionDTO
            {
                Estado = "Error",
                Mensaje = "El QR no pudo ser procesado."
            };
        }
    }
}
