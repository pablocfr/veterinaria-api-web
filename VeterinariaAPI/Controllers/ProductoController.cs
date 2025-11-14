using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService productoService;

        public ProductoController(IProductoService productoService)
        {
            this.productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => productoService.Listado()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await Task.Run(() => productoService.ObtenerPorId(id));
            if (producto == null)
                return NotFound("Producto no encontrado");
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Producto producto)
        {
            var creado = await Task.Run(() => productoService.RegistrarProducto(producto));
            return Ok(creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            producto.IdProducto = id;
            var actualizado = await Task.Run(() => productoService.ActualizarProducto(producto));
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await Task.Run(() => productoService.EliminarProducto(id));

            if (!eliminado)
                return NotFound("No se encontró el producto a eliminar");

            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}
