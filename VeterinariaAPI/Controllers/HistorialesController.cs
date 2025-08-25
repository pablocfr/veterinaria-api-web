using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialesController : Controller
    {
        private readonly IHistorialService _historialService;

        public HistorialesController(IHistorialService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerHistoriales()
        {
            var historiales = await Task.Run(() => _historialService.Listar());
            return Ok(historiales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerHistorialPorId(int id)
        {
            var historial = await Task.Run(() => _historialService.ObtenerPorId(id));
            if (historial == null)
            {
                return NotFound("Historial no encontrado");
            }
            return Ok(historial);
        }

        [HttpGet("search")]
        public async Task<IActionResult> BuscarHistorialPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("El parámetro 'nombre' es requerido");
            }

            var historiales = await Task.Run(() => _historialService.BuscarPorNombre(nombre));

            // Convertir a DTO aquí mismo
            var dtos = historiales.Select(h => new HistorialBusquedaDTO
            {
                IdHistorial = h.IdHistorial,
                NombreMascota = h.NombreMascota,
                EspecieMascota = h.EspecieMascota,
                NombreCliente = h.NombreCliente,
                FechaAtencion = h.FechaAtencion,
                MotivoCita = h.MotivoCita,
                DiagnosticoResumen = h.Diagnostico.Length > 50
                    ? h.Diagnostico.Substring(0, 50) + "..."
                    : h.Diagnostico
            }).ToList();

            return Ok(dtos);
        }
    }
}