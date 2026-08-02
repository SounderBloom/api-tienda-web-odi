using api_tienda_web_odi.Data;
using api_tienda_web_odi.Data.Producto;
using api_tienda_web_odi.Infraestructure;
using api_tienda_web_odi.Models.Productos;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_web_odi.Service
{
    public class ProductosService: IProductosService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductosService(
            AppDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<bool> CrearProducto(CrearProductoDTO producto, Guid vendedorId)
        {
            await using var trsc = await _context.Database.BeginTransactionAsync();

            try
            {
                var productoBD = _context.Producto.Add(new Producto
                {
                    Titulo = producto.Titulo,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Disponible = true,
                    FechaPublicacion = DateTime.Now,
                    TipoTransaccion = producto.TipoTransaccion,
                    Latitud = producto.Latitud,
                    Longitud = producto.Longitud,
                    VendedorId = vendedorId
                });

                // Guarda primero el producto
                await _context.SaveChangesAsync();

                foreach (var foto in producto.Fotos)
                {
                    string nombreArchivo =
                        $"{productoBD.Entity.Id}_{foto.Orden}{Path.GetExtension(foto.Foto.FileName)}";

                    string rutaFisica = Path.Combine(
                        _environment.WebRootPath,
                        "Uploads",
                        "Productos",
                        nombreArchivo);

                    string rutaPublica =
                        $"/Uploads/Productos/{nombreArchivo}";

                    // Guarda el archivo en disco
                    await using (FileStream stream = new(rutaFisica, FileMode.Create))
                    {
                        await foto.Foto.CopyToAsync(stream);
                    }

                    _context.FotosProducto.Add(new FotosProducto
                    {
                        ProductoId = productoBD.Entity.Id,
                        Orden = foto.Orden,
                        FotoRuta = rutaPublica
                    });
                }

                await _context.SaveChangesAsync();
                await trsc.CommitAsync();

                return true;
            }
            catch
            {
                await trsc.RollbackAsync();
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

        public List<TipoTransaccionDTO> ObtenerTiposTransaccion() =>
            Enum.GetValues<TipoTransaccion>()
                .Select(t => new TipoTransaccionDTO
                {
                    Id = (int)t,
                    TipoTransaccion = t.ToString().Replace("OVenta", " o Venta")
                })
            .ToList();

    }
}
