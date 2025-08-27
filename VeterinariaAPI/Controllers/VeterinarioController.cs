using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinarioController : ControllerBase
    {
        private readonly IVeterinarioService _veterinarioService;

        public VeterinarioController(IVeterinarioService veterinarioService)
        {
            _veterinarioService = veterinarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => _veterinarioService.Listado()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var veterinario = await Task.Run(() => _veterinarioService.ObtenerPorId(id));
            if (veterinario == null)
                return NotFound("Veterinario no encontrado");
            return Ok(veterinario);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] Veterinario veterinario)
        {
            var creado = await Task.Run(() => _veterinarioService.RegistrarVeterinario(veterinario));
            return Ok(creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Veterinario veterinario)
        {
            veterinario.IDVeterinario = id;
            var actualizado = await Task.Run(() => _veterinarioService.ActualizarVeterinario(veterinario));
            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await Task.Run(() => _veterinarioService.EliminarVeterinario(id));

            if (!eliminado)
                return NotFound("No se encontró el veterinario a eliminar");

            return Ok(new { mensaje = "Veterinario eliminado correctamente" });
        }
    }
}
