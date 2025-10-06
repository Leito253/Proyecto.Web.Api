using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Proyecto.Modelos.Entidades;
using Proyecto.Modelos.Repositorios;
using Aspose.Drawing.Imaging;
using Proyecto.Modelos.Interfaces;
using System.Drawing;


namespace Proyecto.Modelos.Servicios;

public class QrService
{
    private readonly IEntradaRepository _entradaRepo;
    private readonly IConfiguration _config;

    public QrService(IEntradaRepository entradaRepo, IConfiguration config)
    {
        _entradaRepo = entradaRepo;
        _config = config;
    }

    public string GenerarQrEntrada(int entradaId)
    {
        var entrada = _entradaRepo.GetById(entradaId);
        if (entrada == null) return string.Empty;

        // Payload (puede incluir firma digital)
        string firmaSecreta = _config["Qr:Key"];
        string payload = $"{entrada.idEntrada}|{entrada.idFuncion}|{entrada.idCliente}|{firmaSecreta}";

      
        using var ms = new MemoryStream();
  
        return Convert.ToBase64String(ms.ToArray());
    }

    public string ValidarQr(string qrContent)
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

        var funcion = entrada.funcion; // suponiendo que Entrada tiene navegación
        if (funcion != null && funcion.FechaHora < DateTime.Now) return "Expirada";

        // marcar como usada
        entrada.Usada = true;
        _entradaRepo.Update(entrada);

        return "Ok";
    }
}
