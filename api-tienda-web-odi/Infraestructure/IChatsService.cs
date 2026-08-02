namespace api_tienda_web_odi.Infraestructure
{
    public interface IChatsService
    {
        Task<bool> CrearChat(Guid InteresadoId, Guid ProductoId);
    }
}
