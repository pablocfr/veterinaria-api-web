using Capa_Entidad.Entidades;
using Capa_Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.DTOs;

namespace VeterinariaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        // GET /api/clientes
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _service.ListarClientes();
            return Ok(list);
        }

        // GET /api/clientes/activos
        [HttpGet("activos")]
        public IActionResult GetActivos()
        {
            var list = _service.ListarClientes()
                               .Where(c => c.Estado) // solo activos
                               .ToList();
            return Ok(list);
        }


        // GET /api/clientes/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _service.ObtenerPorId(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        // GET /api/clientes/search?nombre=Pedro
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El nombre es requerido.");
            var list = _service.BuscarPorNombre(nombre);
            return Ok(list);
        }

        // GET /api/clientes/activos/search?nombre=Pedro
        [HttpGet("activos/search")]
        public IActionResult SearchActivos([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest("El nombre es requerido.");

            var list = _service.BuscarPorNombre(nombre)
                               .Where(c => c.Estado) // solo activos
                               .ToList();
            return Ok(list);
        }

        // POST /api/clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] ClienteRegistrarDTO dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                DNI = dto.DNI,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Correo = dto.Correo,
                Estado = true // por defecto activo
            };

            var creado = _service.RegistrarCliente(cliente);   // sp_CrearCliente devuelve Id
            return CreatedAtAction(nameof(GetById), new { id = creado.IdCliente }, creado);
        }

        // PUT /api/clientes/{id}
        [HttpPut("{id:int}")]
        public IActionResult ActualizarCliente(int id, [FromBody] ClienteActualizarDTO dto)
        {
            if (id != dto.IdCliente)
                return BadRequest("El id de la ruta y del body deben coincidir.");

            var cli = new Cliente
            {
                IdCliente = dto.IdCliente,
                Nombre = dto.Nombre,
                DNI = dto.DNI,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Correo = dto.Correo,
                Estado = dto.Estado
            };

            _service.ActualizarCliente(cli); // sp_ActualizarCliente
            return Ok("Cliente actualizado correctamente");
        }

        // DELETE /api/clientes/{id}  (delete lógico)
        [HttpDelete("{id:int}")]
        public IActionResult EliminarCliente(int id)
        {
            _service.Eliminar(id);   // sp_EliminarCliente -> estado = 0
            return Ok("Cliente eliminado correctamente");
        }
    }
}
