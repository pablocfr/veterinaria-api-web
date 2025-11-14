using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VeterinariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IProximaCitaService _proximacitaService;

        public DashboardController(IDashboardService dashboardService, IProximaCitaService proximacitaService)
        {
            _dashboardService = dashboardService;
            _proximacitaService = proximacitaService;
        }
        [HttpGet("totales")]
        public async Task<IActionResult> Totales()
        {
            var totales = await Task.Run(() => _dashboardService.totalesDashboard());
            return Ok(totales);
        }

        [HttpGet("proximas-citas")]
        public async Task<IActionResult> ProximasCitas()
        {
            var proximacita = await Task.Run(() => _proximacitaService.listarProximasCitas());
            return Ok(proximacita);
        }
        [HttpGet]
        [Route("busqueda/{id}")]
        public async Task<IActionResult> BusquedaPorId(int id)
        {
            return Ok(await Task.Run(() => _proximacitaService.ObtenerPorId(id)));
        }
    }

    }

