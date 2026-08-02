namespace api_tienda_web_odi.Data.Chats
{
    public class ArchivosEnMensaje
    {
        public int Id { get; set; }
        public int MensajeId { get; set; }
        public MensajeChat Mensaje { get; set; } = new MensajeChat();
        public int ArchivoId { get; set; }
        public ArchivosMensaje Archivo { get; set; } = new ArchivosMensaje();
    }
}
