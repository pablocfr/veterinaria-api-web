using Capa_Datos.Repositorio;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

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

        [HttpGet]
        [Route("busquedaFecha/{fecha}")]
        public async Task<IActionResult> BusquedaPorFecha(DateTime fecha)
        {
            return Ok(await Task.Run(() => _citaService.ListarPorFecha(fecha)));
        }

        [HttpPost]
        public IActionResult CrearCita([FromBody] CitaRegistrarDTO dto)
        {

            var cita = new Cita
            {
                Fecha = dto.Fecha,
                Motivo = dto.Motivo,
                IdMascota = dto.IdMascota,
                IdVeterinario = dto.IdVeterinario,
            };

            _citaService.Registrar(cita);

            return Ok("Cita creada correctamente");
        }

        [HttpPut]
        public IActionResult ActualizarCita([FromBody] CitaActualizarDTO dto)
        {
            var cita = new Cita
            {
                IdCita = dto.IdCita,
                Fecha = dto.Fecha,
                Motivo = dto.Motivo,
                IdMascota = dto.IdMascota,
                IdVeterinario = dto.IdVeterinario
            };

            _citaService.Actualizar(cita);

            return Ok("Cita actualizada correctamente");
        }

        [HttpDelete]
        public IActionResult EliminarCita(int id)
        {
            _citaService.Eliminar(id);
            return Ok("Cita elimina correctamente");
        }

        [HttpGet]
        [Route("/cantidad-citas")]
        public async Task<IActionResult> CantidadCitas()
        {
            return Ok(await Task.Run(() => _citaService.Listar().Count()));
        }
    }
}

