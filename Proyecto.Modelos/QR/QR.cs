namespace Proyecto.Modelos.Entidades
{
    public class Qr
    {
        public int IdQr { get; set; }
        public int IdEntrada { get; set; }  
        public string QrContent { get; set; } = string.Empty;  
        public DateTime FechaGeneracion { get; set; }  
        public bool Usado { get; set; }  
        public string Estado { get; set; } = "NoValidado";  

    
        public Entrada Entrada { get; set; }  
    }
}
