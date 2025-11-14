using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : Controller
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => _proveedorService.Listar()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            return Ok(await Task.Run(() => _proveedorService.ObtenerPorId(id)));
        }

        [HttpPost]
        public IActionResult CrearProveedor([FromBody] ProveedorRegistrarDTO dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Ruc = dto.Ruc,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Correo = dto.Correo
            };

            _proveedorService.Registrar(proveedor);
            return Ok("Proveedor creado correctamente");
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarProveedor(int id, [FromBody] ProveedorActualizarDTO dto)
        {
            var proveedor = new Proveedor
            {
                IdProveedor = id,
                Nombre = dto.Nombre,
                Ruc = dto.Ruc,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Correo = dto.Correo
            };

            _proveedorService.Actualizar(proveedor);
            return Ok("Proveedor actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProveedor(int id)
        {
            var proveedorExistente = _proveedorService.ObtenerPorId(id);
            if (proveedorExistente == null)
            {
                return NotFound("Proveedor no encontrado");
            }

            _proveedorService.Eliminar(id);
            return Ok("Proveedor eliminado correctamente (eliminación lógica)");
        }
    }
}