using api_tienda_web_odi.Data;
using api_tienda_web_odi.Infraestructure;
using api_tienda_web_odi.Models.Productos;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_web_odi.Service
{
    public class ProductosService: IProductosService
    {
        private readonly AppDbContext _context;
        public ProductosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearProducto(ProductoDTO producto, Guid VendedorId)
        {
            var trsc = _context.Database.BeginTransaction();
            try
            {
                _context.Producto.Add(new Data.Producto.Producto
                {
                    Titulo = producto.Titulo,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Disponible = true,
                    FechaPublicacion = DateTime.Now,
                    TipoTransaccion = producto.TipoTransaccion,
                    Latitud = producto.Latitud,
                    Longitud = producto.Longitud,
                    VendedorId = VendedorId
                });
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    await trsc.RollbackAsync();
                    return false;
                }
                await trsc.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await trsc.RollbackAsync();
                _ = ex;
                return false;
            }
        }

        public async Task<bool> EliminarProducto(Guid productoId, Guid userId)
        {
            var trsc = _context.Database.BeginTransaction();
            try
            {
                var productoBd = await _context.Producto.FirstOrDefaultAsync(p => p.Id == productoId && p.VendedorId == userId);
                if (productoBd == null)
                {
                    return false;
                }

                _context.Producto.Remove(productoBd);
                var result = await _context.SaveChangesAsync();
                if (result <= 0)
                {
                    await trsc.RollbackAsync();
                    return false;
                }
                await trsc.CommitAsync();
                return true;
            }
            catch (Exception ex) {
                await trsc.RollbackAsync();
                _ = ex;
                return false;
            }
        }

        public async Task<List<ProductoDTO>> ObtenerProductos()
        {
            var productos = await _context.Producto
                .Where(p => p.Disponible)
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    TipoTransaccion = p.TipoTransaccion,
                    Latitud = p.Latitud,
                    Longitud = p.Longitud,
                    VendedorId = p.VendedorId,
                    FechaPublicacion = p.FechaPublicacion,
                    Disponible = p.Disponible
                })
                .ToListAsync();
            return productos;
        }

        public async Task<List<ProductoDTO>> ObtenerProductosDeUsuario(Guid UsuarioId)
        {
            var productos = await _context.Producto
                .Where(p => p.VendedorId == UsuarioId)
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    TipoTransaccion = p.TipoTransaccion,
                    Latitud = p.Latitud,
                    Longitud = p.Longitud,
                    VendedorId = p.VendedorId,
                    FechaPublicacion = p.FechaPublicacion,
                    Disponible = p.Disponible
                })
                .ToListAsync();
            return productos;
        }
    }
}
