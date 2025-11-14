using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            this._ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => _ventaService.ListDetalleVentas()));
        }

        [HttpGet]
        [Route("busqueda/{id}")]
        public async Task<IActionResult> BusquedaPorId(int id)
        {
            return Ok(await Task.Run(() => _ventaService.ObtenerVentaPorId(id)));
        }

        [HttpGet]
        [Route("busqueda/cabecera/{id}")]
        public async Task<IActionResult> ObtenerVentaCabeceraPorId(int id)
        {
            return Ok(await Task.Run(() => _ventaService.obtenerVentaCabecera(id)));
        }

        [HttpGet]
        [Route("busqueda/cabecera/detalle/{id}")]
        public async Task<IActionResult> ObtenerDetalleVentaPorIdVenta(int id)
        {
            return Ok(await Task.Run(() => _ventaService.obtenerDetalleVentaPorIdVenta(id)));
        }

        [HttpPost]
        public IActionResult CrearVenta([FromBody] VentasRegistrarDTO dto)
        {
            var detalles = dto.Detalles.Select(d => new Detalle_Venta
            {
                IdProducto = d.IdProducto,
                IdServicio = d.IdServicio,
                Cantidad = d.Cantidad,
                SubTotal = d.SubTotal
            }).ToList();

            var mensaje = _ventaService.GrabarVenta(dto.IdCliente, dto.Total, detalles);

            // Devolver JSON
            return Ok(new { Mensaje = mensaje });
        }
    }
}
