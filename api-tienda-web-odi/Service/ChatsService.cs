using api_tienda_web_odi.Data;
using api_tienda_web_odi.Data.Chats;
using api_tienda_web_odi.Data.Producto;
using api_tienda_web_odi.Infraestructure;
using api_tienda_web_odi.Models.Chats;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_web_odi.Service
{
    public class ChatsService : IChatsService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ChatsService(
            AppDbContext context, 
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public async Task<bool> EnviarMensaje(CrearMensajeDTO Mensaje, Guid EmisorId)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //Saber si es Vendedor o Comprador quien envia el mensaje
                EmisorMensaje ValorEmisorMensaje = EmisorMensaje.Sistema; //Empieza en el supuesto que es un mensaje de Sistema

                if (!Mensaje.EsSistema) //Si no es del sistema vamos a determinar de quien es
                {
                    var chatOrigen = await _context.Chat
                        .Include(c => c.Producto)
                        .FirstOrDefaultAsync(x => x.Id == Mensaje.ChatId);

                    if (chatOrigen == null || chatOrigen.Producto == null)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                    if (chatOrigen.CompradorId == EmisorId)
                    {
                        ValorEmisorMensaje = EmisorMensaje.Comprador;

                    }
                    else if (chatOrigen.Producto.VendedorId == EmisorId)
                    {
                        ValorEmisorMensaje = EmisorMensaje.Vendedor;
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }

                //Crear el mensaje en BD
                var MensajeBD = await _context.MensajeChat.AddAsync(new MensajeChat
                {
                    ChatId = Mensaje.ChatId,
                    Estado = 0,
                    Contenido = Mensaje.Mensaje,
                    Emisor = ValorEmisorMensaje
                });

                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                //Agregar los archivos al mensaje si los hay
                var contador = 0;
                foreach (var archivo in Mensaje.Archivos)
                {
                    contador++;
                    string nombreArchivo =
                        $"{MensajeBD.Entity.Id}_{contador}{Path.GetExtension(archivo.FileName)}";

                    string rutaFisica = Path.Combine(
                        _environment.ContentRootPath,
                        "PrivateUserFiles",
                        "ArchivosMensajes",
                        nombreArchivo);

                    // Guarda el archivo en disco
                    await using (FileStream stream = new(rutaFisica, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }

                    _context.ArchivosMensaje.Add(new ArchivosMensaje
                    {
                        MensajeId = MensajeBD.Entity.Id,
                        NombreArchivo = nombreArchivo
                    });
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _ = ex;
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
