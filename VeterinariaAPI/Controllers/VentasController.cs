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

        [HttpPost]
        public IActionResult CrearVenta([FromBody] VentasRegistrarDTO dto)
        {
            // Armar los detalles
            var detalles = dto.Detalles.Select(d => new Detalle_Venta
            {
                IdProducto = d.IdProducto,
                IdServicio = d.IdServicio,
                Cantidad = d.Cantidad
            }).ToList();

            detalles.Count();

            // Llamar al método del repositorio/servicio
            var resultado = _ventaService.GrabarVenta(dto.IdCliente, dto.Total, detalles);

            return Ok(resultado);
        }
    }
}
