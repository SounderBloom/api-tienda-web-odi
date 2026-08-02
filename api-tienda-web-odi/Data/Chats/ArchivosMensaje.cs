namespace api_tienda_web_odi.Data.Chats
{
    public class ArchivosMensaje
    {
        public int Id { get; set; }
        public string Archivo { get; set; } = string.Empty;
        public List<ArchivosEnMensaje> ArchivosEnMensaje { get; set; } = new();
    }
}
