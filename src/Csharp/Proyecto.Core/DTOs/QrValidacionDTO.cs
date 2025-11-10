namespace Proyecto.Core.DTOs
{
    public class QrValidacionDTO
    {
        public bool EsValido { get; set; }
        public string Mensaje { get; set; }
        public EntradaDTO? Entrada { get; set; }
    }
}
