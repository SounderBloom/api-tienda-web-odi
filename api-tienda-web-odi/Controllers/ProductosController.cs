using api_tienda_web_odi.Infraestructure;
using api_tienda_web_odi.Models.Productos;
using api_tienda_web_odi.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace api_tienda_web_odi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _productosService;
        public ProductosController(IProductosService productosService) {
            _productosService = productosService;
        }

        [HttpPost("Crear")]
        [Authorize]
        public async Task<IActionResult> CrearProducto([FromForm] CrearProductoDTO producto)
        {
            var vendedorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());

            var result = await _productosService.CrearProducto(producto, vendedorId);

            if (!result)
            {
                return BadRequest(new ResponseWrapper<bool>
                {
                    Data = false,
                    Message = "Error al crear el producto",
                    Code = HttpStatusCode.BadRequest
                });
            }
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = "Producto creado exitosamente",
                Code = HttpStatusCode.OK
            });
        }

        [HttpGet("MisProductos")]
        [Authorize]
        public async Task<IActionResult> ObtenerMisProductos()
        {
            var UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            var productos = await _productosService.ObtenerProductosDeUsuario(UserId);
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = "Productos obtenidos exitosamente",
                Code = HttpStatusCode.OK
            });
        }

        [HttpDelete("Eliminar")]
        [Authorize]
        public async Task<IActionResult> EliminarProducto(Guid ProductoId)
        {
            var UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
            var result = await _productosService.EliminarProducto(ProductoId, UserId);
            if (!result)
            {
                return BadRequest(new ResponseWrapper<bool> {
                    Data = false,
                    Message = "Error al eliminar el producto",
                    Code = HttpStatusCode.BadRequest
                });
            }
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = "Producto eliminado exitosamente",
                Code = HttpStatusCode.OK
            });
        }

        [HttpGet("TiposTransaccion")]
        public async Task<IActionResult> ObtenerTiposTransaccion()
        {
            var tiposTransaccion = _productosService.ObtenerTiposTransaccion();
            return Ok(new ResponseWrapper<List<TipoTransaccionDTO>>
            {
                Data = tiposTransaccion,
                Message = "Tipos de transacción obtenidos exitosamente",
                Code = HttpStatusCode.OK
            });
        }



        [HttpGet("ObtenerTodo")]
        [Authorize]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productosService.ObtenerProductos();
            return Ok(new ResponseWrapper<bool>
            {
                Data = true,
                Message = "Productos obtenidos exitosamente",
                Code = HttpStatusCode.OK
            });
        }
    }
}
