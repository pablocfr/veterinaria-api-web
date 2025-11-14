using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class MascotaMvcController : Controller
    {
        private readonly IConfiguration _config;

        public MascotaMvcController(IConfiguration config)
        {
            _config = config;
        }

        // ===== Helpers HTTP =====

        private List<Mascota> ObtenerMascotasDesdeApi()
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

        private Mascota ObtenerMascotaPorIdDesdeApi(int id)
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

        private void RegistrarMascotaEnApi(Mascota mascota)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(JsonConvert.SerializeObject(new
                {
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

        private void ActualizarMascotaEnApi(Mascota mascota)
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

        private bool EliminarMascotaEnApi(int id)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.DeleteAsync($"Mascotas/{id}").Result;   // DELETE /api/Mascotas/{id}
                return resp.IsSuccessStatusCode;
            }
        }

        private List<Mascota> BuscarMascotasPorNombreDesdeApi(string nombre)
        {
            var listado = new List<Mascota>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync($"Mascotas/search?nombre={Uri.EscapeDataString(nombre)}").Result;
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Mascota>>(data);
                }
            }
            return listado;
        }

        private List<Cliente> ObtenerClientesDesdeApi()
        {
            var listado = new List<Cliente>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync("Clientes").Result;            // GET /api/Clientes
                if (resp.IsSuccessStatusCode)
                {
                    var data = resp.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Cliente>>(data);
                }
            }
            return listado;
        }

        private Cliente? ObtenerClientePorIdDesdeApi(int id)
        {
            Cliente? cli = null;
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
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

        public IActionResult Index(string? nombre, int page = 1)
        {
            var listado = string.IsNullOrWhiteSpace(nombre)
                ? ObtenerMascotasDesdeApi()
                : BuscarMascotasPorNombreDesdeApi(nombre);

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

        public IActionResult Details(int id)
        {
            var mascota = ObtenerMascotaPorIdDesdeApi(id);
            if (mascota == null) return NotFound();
            return View(mascota);
        }

        public IActionResult Create()
        {
            var clientesActivos = ObtenerClientesDesdeApi()
                .Where(c => c.Estado)
                .OrderBy(c => c.Nombre)
                .ToList();

            ViewBag.Clientes = new SelectList(clientesActivos, "IdCliente", "Nombre");
            ViewBag.SinClientesActivos = !clientesActivos.Any();

            return View(new Mascota { Estado = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                var clientesActivos = ObtenerClientesDesdeApi()
                    .Where(c => c.Estado)
                    .OrderBy(c => c.Nombre)
                    .ToList();

                ViewBag.Clientes = new SelectList(clientesActivos, "IdCliente", "Nombre", mascota.IdCliente);
                ViewBag.SinClientesActivos = !clientesActivos.Any();

                return View(mascota);
            }

            RegistrarMascotaEnApi(mascota);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mascota mascota)
        {
            ActualizarMascotaEnApi(mascota);
            return RedirectToAction("Details", new { id = mascota.IdMascota });
        }

        public IActionResult Edit(int id)
        {
            var mascota = ObtenerMascotaPorIdDesdeApi(id);

            return View(mascota);
        }

        public IActionResult Delete(int id)
        {
            var mascota = ObtenerMascotaPorIdDesdeApi(id);
            if (mascota == null) return NotFound();
            return View(mascota);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarMascotaEnApi(id);
            return RedirectToAction("Index");
        }
    }
}
