using api_tienda_web_odi.Data.Auth;

namespace api_tienda_web_odi.Data.Chats
{
    public class MensajeChat
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; } = new();
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.Now;
        public EmisorMensaje Emisor { get; set; } = 0;
        public EstadoMensaje Estado { get; set; } = 0;
        public List<ArchivosEnMensaje> ArchivosEnMensaje { get; set; } = new();
    }
}
