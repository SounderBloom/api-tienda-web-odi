using api_tienda_web_odi.Data.Chats;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_tienda_web_odi.Data.Auth
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoPaterno { get; set; }
        public required string ApellidoMaterno { get; set; }
        public required string Correo { get; set; }
        public string FotoPerfilUrl { get; set; } = "/Uploads/Usuarios/default.webp";
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public bool EmailConfirmado { get; set; }
        public required string PasswordHash { get; set; }
        public Rol Rol { get; set; }

        [InverseProperty("Vendedor")]
        public List<Producto.Producto> Productos { get; set; } = [];

        [InverseProperty("Comprador")]
        public virtual List<Chat> ChatsComprador { get; set; } = [];
    }
}
