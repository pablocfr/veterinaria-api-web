using System.Data;
using Capa_Datos.Mapper;
using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        private readonly IMascotaService _service;

        public MascotasController(IMascotaService service)
        {
            _service = service;
        }

        // GET /api/mascotas
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _service.listarMascotas();
            return Ok(list);
        }

        // GET /api/mascotas/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _service.ObtenerPorId(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        // GET /api/mascotas/search?nombre=Firulais
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El nombre es requerido.");
            var list = _service.BuscarPorNombre(nombre);
            return Ok(list);
        }

        // POST /api/mascotas
        [HttpPost]
        public IActionResult CrearMascota([FromBody] MascotaRegistrarDTO dto)
        {
            var Mascota = new Mascota
            {
                Nombre = dto.Nombre,
                Especie = dto.Especie,
                Raza = dto.Raza,
                Edad = dto.Edad,
                Sexo = dto.Sexo,
                IdCliente = dto.IdCliente
            };

            var creada = _service.RegistrarMascota(Mascota);               // SP_InsertMascota
            return CreatedAtAction(nameof(GetById), new { id = creada.IdMascota }, dto);
        }

        // PUT /api/mascotas/{id}
        [HttpPut("{id:int}")]
        public IActionResult ActualizarMascota(int id, [FromBody] MascotaActualizarDTO dto)
        {
            if (id != dto.IdMascota)
                return BadRequest("El id de la ruta y del body deben coincidir.");
            var Mascota = new Mascota
            {
                IdMascota = dto.IdMascota,
                Nombre = dto.Nombre,
                Especie = dto.Especie,
                Raza = dto.Raza,
                Edad = dto.Edad,
                Sexo = dto.Sexo
            };
            _service.ActualizarMascota(Mascota);                      // SP_UpdateMascota
            return Ok("Mascota actualizada correctamente");
        }

        // DELETE /api/mascotas/{id}
        [HttpDelete("{id:int}")]
        public IActionResult EliminarMascota(int id)
        {
            _service.Eliminar(id);          // invoca Sp_EliminarMascota
            return Ok("Mascota eliminada correctamente");
        }
    }
}
