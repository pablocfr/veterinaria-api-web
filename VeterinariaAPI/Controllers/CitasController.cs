using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => _citaService.Listar()));
        }

        [HttpGet]
        [Route("busqueda/{id}")]
        public async Task<IActionResult> BusquedaPorId(int id)
        {
            return Ok(await Task.Run(() => _citaService.ObtenerPorId(id)));
        }
    }
}

