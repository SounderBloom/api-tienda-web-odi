using api_tienda_web_odi.Data.Auth;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_web_odi.Data.Chats
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public Guid? ProductoId { get; set; }
        public Producto.Producto? Producto { get; set; }
        public Guid? CompradorId { get; set; }
        public Usuario? Comprador { get; set; }
        public string NombreProductoSnapshot { get; set; } = string.Empty;
        public string ImagenProductoSnapshot { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal PrecioProductoSnapshot { get; set; }
        public List<MensajeChat> MensajeChat { get; set; } = new();
    }
}
