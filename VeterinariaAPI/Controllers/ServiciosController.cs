using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : Controller
    {
        private readonly IServicioService _servicioService;

        public ServiciosController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => _servicioService.Listar()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            return Ok(await Task.Run(() => _servicioService.ObtenerPorId(id)));
        }

        [HttpPost]
        public IActionResult CrearServicio([FromBody] ServicioRegistrarDTO dto)
        {
            var servicio = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio
            };

            _servicioService.Registrar(servicio);
            return Ok("Servicio creado correctamente");
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarServicio(int id, [FromBody] ServicioActualizarDTO dto)
        {
            var servicio = new Servicio
            {
                IdServicio = id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio
            };

            _servicioService.Actualizar(servicio);
            return Ok("Servicio actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarServicio(int id)
        {
            _servicioService.Eliminar(id);
            return Ok("Servicio eliminado correctamente (eliminación lógica)");
        }
    }
}