using api_tienda_web_odi.Data;
using api_tienda_web_odi.Data.Chats;
using api_tienda_web_odi.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_web_odi.Service
{
    public class ChatsService : IChatsService
    {
        private readonly AppDbContext _context;
        public ChatsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearChat(Guid InteresadoId, Guid ProductoId)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Id == ProductoId);

                _context.Chat.Add(new Chat
                {
                    CompradorId = InteresadoId,
                    ProductoId = ProductoId,
                    ImagenProductoSnapshot = "",
                    NombreProductoSnapshot = producto.Titulo,
                    PrecioProductoSnapshot = producto.Precio,
                });

                var result = await _context.SaveChangesAsync();
                if (result <= 0) {
                    transaction.Rollback();
                    return false;
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _ = ex;
                transaction.Rollback();
                return false;
            }
        }
    }
}
