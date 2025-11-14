using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class MascotaController : Controller
    {
        private readonly IConfiguration _config;

        public MascotaController(IConfiguration config)
        {
            _config = config;
        }

        // ===== Helpers HTTP =====

        private List<Mascota> ObtenerMascotas()
        {
            var listado = new List<Mascota>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);   
                var resp = http.GetAsync("Mascotas").Result;           // GET /api/Mascotas
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Mascota>>(data);
                }
            }
            return listado;
        }

        private Mascota ObtenerPorId(int id)
        {
            Mascota mascota = null;
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync($"Mascotas/{id}").Result;      // GET /api/Mascotas/{id}
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    mascota = JsonConvert.DeserializeObject<Mascota>(data);
                }
            }
            return mascota;
        }

        private void RegistrarMascota(Mascota mascota)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(JsonConvert.SerializeObject(new
                {
                    // API espera MascotaRegistrarDTO
                    mascota.Nombre,
                    mascota.Especie,
                    mascota.Raza,
                    mascota.Edad,
                    mascota.Sexo,
                    mascota.IdCliente
                }), Encoding.UTF8, "application/json");

                var resp = http.PostAsync("Mascotas", contenido).Result; // POST /api/Mascotas
                resp.EnsureSuccessStatusCode();
            }
        }

        private void ActualizarMascota(Mascota mascota)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(JsonConvert.SerializeObject(new
                {
                    IdMascota = mascota.IdMascota,
                    mascota.Nombre,
                    mascota.Especie,
                    mascota.Raza,
                    mascota.Edad,
                    mascota.Sexo
                }), Encoding.UTF8, "application/json");

                var resp = http.PutAsync($"Mascotas/{mascota.IdMascota}", contenido).Result;
                resp.EnsureSuccessStatusCode();
            }
        }

        private bool EliminarMascota(int id)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.DeleteAsync($"Mascotas/{id}").Result;   // DELETE /api/Mascotas/{id}
                return resp.IsSuccessStatusCode;
            }
        }
        private List<VeterinariaApp.Models.Mascota> BuscarMascotasPorNombre(string nombre)
        {
            var listado = new List<VeterinariaApp.Models.Mascota>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]); 
                var resp = http.GetAsync($"Mascotas/search?nombre={Uri.EscapeDataString(nombre)}").Result;
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<VeterinariaApp.Models.Mascota>>(data);
                }
            }
            return listado;
        }


        //obtener clientes para mostrar nombre en el Edit/Create
        private List<Cliente> ObtenerClientes()
        {
            var listado = new List<Cliente>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                // Ajusta la ruta si tu endpoint es distinto (p. ej. "Clientes/Activos")
                var resp = http.GetAsync("Clientes").Result;            // GET /api/Clientes
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Cliente>>(data);
                }
            }
            return listado;
        }

        private Cliente? ObtenerClientePorId(int id)
        {
            Cliente? cli = null;
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                // Ajusta la ruta si tu API de clientes es distinta
                var resp = http.GetAsync($"Clientes/{id}").Result;   // GET /api/Clientes/{id}
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<Cliente>(data);
                }
            }
            return cli;
        }

        // ===== Acciones MVC =====

        // GET: /Mascota
        public IActionResult Index(string? nombre, int page = 1)
        {
            // Si viene nombre -> busca; si no -> lista todas
            var listado = string.IsNullOrWhiteSpace(nombre)
                ? ObtenerMascotas()
                : BuscarMascotasPorNombre(nombre);

            int registrosPorPagina = 10;
            int totalRegistros = listado.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            totalPaginas = Math.Max(1, totalPaginas);
            page = Math.Min(Math.Max(1, page), totalPaginas);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;
            ViewBag.nombre = nombre ?? string.Empty;

            return View(listado.Skip((page - 1) * registrosPorPagina).Take(registrosPorPagina));
        }


        // GET: /Mascota/Details/5
        public IActionResult Details(int id)
        {
            var mascota = ObtenerPorId(id);
            if (mascota == null) return NotFound();
            return View(mascota);
        }

        // GET: /Mascota/Create
        public IActionResult Create()
        {
            // Toma solo clientes activos y ordena por nombre
            var clientesActivos = ObtenerClientes()
                .Where(c => c.Estado)
                .OrderBy(c => c.Nombre)
                .ToList();

            ViewBag.Clientes = new SelectList(clientesActivos, "IdCliente", "Nombre");
            ViewBag.SinClientesActivos = !clientesActivos.Any();

            // Estado por defecto true (opcional)
            return View(new Mascota { Estado = true });
        }
        // POST: /Mascota/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                var clientesActivos = ObtenerClientes()
                    .Where(c => c.Estado)
                    .OrderBy(c => c.Nombre)
                    .ToList();

                ViewBag.Clientes = new SelectList(clientesActivos, "IdCliente", "Nombre", mascota.IdCliente);
                ViewBag.SinClientesActivos = !clientesActivos.Any();

                return View(mascota);
            }

            RegistrarMascota(mascota);
            return RedirectToAction("Index");
        }

        // POST: /Mascota/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mascota mascota)
        {
            ActualizarMascota(mascota);
            return RedirectToAction("Details", new { id = mascota.IdMascota });
        }

        // GET: /Mascota/Edit/5
        public IActionResult Edit(int id)
        {
            var mascota = ObtenerPorId(id);

            return View(mascota);
        }

        // GET: /Mascota/Delete/5
        public IActionResult Delete(int id)
        {
            var mascota = ObtenerPorId(id);
            if (mascota == null) return NotFound();
            return View(mascota);
        }

        // POST: /Mascota/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarMascota(id);
            return RedirectToAction("Index");
        }
    }
}
