// using QRCoder;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.IO;
// using System.Text;
// using Proyecto.Modelos.Entidades;
// using Proyecto.Modelos.Repositorios;

// namespace Proyecto.Modelos.Servicios;

// public class QrService
// {
//     private readonly IEntradaRepository _entradaRepo;
//     private readonly IConfiguration _config;

//     public QrService(IEntradaRepository entradaRepo, IConfiguration config)
//     {
//         _entradaRepo = entradaRepo;
//         _config = config;
//     }

//     public string GenerarQrEntrada(int entradaId)
//     {
//         var entrada = _entradaRepo.GetById(entradaId);
//         if (entrada == null) return string.Empty;

//         // Payload (puede incluir firma digital)
//         string firmaSecreta = _config["Qr:Key"];
//         string payload = $"{entrada.IdEntrada}|{entrada.IdFuncion}|{entrada.IdCliente}|{firmaSecreta}";

//         using var qrGenerator = new QRCodeGenerator();
//         using var qrData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
//         using var qrCode = new QRCode(qrData);
//         using Bitmap bitmap = qrCode.GetGraphic(20);

//         using var ms = new MemoryStream();
//         bitmap.Save(ms, ImageFormat.Png);
//         return Convert.ToBase64String(ms.ToArray());
//     }

//     public string ValidarQr(string qrContent)
//     {
//         var partes = qrContent.Split('|');
//         if (partes.Length < 4) return "FirmaInvalida";

//         int entradaId = int.Parse(partes[0]);
//         int funcionId = int.Parse(partes[1]);
//         int clienteId = int.Parse(partes[2]);
//         string firma = partes[3];

//         string firmaEsperada = _config["Qr:Key"];
//         if (firma != firmaEsperada) return "FirmaInvalida";

//         var entrada = _entradaRepo.GetById(entradaId);
//         if (entrada == null) return "NoExiste";

//         if (entrada.Usada) return "YaUsada";

//         var funcion = entrada.Funcion; // suponiendo que Entrada tiene navegación
//         if (funcion != null && funcion.Fecha < DateTime.Now) return "Expirada";

//         // marcar como usada
//         entrada.Usada = true;
//         _entradaRepo.Update(entrada);

//         return "Ok";
//     }
// }
