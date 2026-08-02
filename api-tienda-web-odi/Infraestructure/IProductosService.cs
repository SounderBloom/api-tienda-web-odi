using api_tienda_web_odi.Models.Productos;

namespace api_tienda_web_odi.Infraestructure
{
    public interface IProductosService
    {
        Task<bool> CrearProducto(CrearProductoDTO producto, Guid VendedorId);
        Task<bool> EliminarProducto(Guid productoId, Guid userId);
        Task<List<ProductoDTO>> ObtenerProductos();
        Task<List<ProductoDTO>> ObtenerProductosDeUsuario(Guid UsuarioId);
        List<TipoTransaccionDTO> ObtenerTiposTransaccion();
    }
}
