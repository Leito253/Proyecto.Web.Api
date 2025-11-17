namespace Proyecto.Core.DTOs
{
    public class OrdenCreateDTO
    {
        public int idCliente { get; set; }
        public List<int> idFunciones { get; set; } = new();
        public List<int> idTarifas { get; set; } = new();
        public List<int> Cantidades { get; set; } = new();
        public int IdEntrada { get; set; }
    }
}
