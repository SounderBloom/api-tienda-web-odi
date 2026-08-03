using api_tienda_web_odi.Models.Chats;

namespace api_tienda_web_odi.Infraestructure
{
    public interface IChatsService
    {
        Task<bool> CrearChat(Guid InteresadoId, Guid ProductoId);
        Task<bool> EnviarMensaje(CrearMensajeDTO Mensaje, Guid EmisorId);
    }
}
