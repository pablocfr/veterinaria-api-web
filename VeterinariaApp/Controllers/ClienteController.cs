using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class ClienteMvcController : Controller
    {
        private readonly IConfiguration _config;

        public ClienteMvcController(IConfiguration config)
        {
            _config = config;
        }

        // ================== Helpers HTTP ==================

        private List<Cliente> ObtenerClientesDesdeApi()
        {
            var lista = new List<Cliente>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync("Clientes").Result; // GET /api/Clientes
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();
                }
            }
            return lista;
        }

        private List<Cliente> BuscarClientesPorNombreDesdeApi(string nombre)
        {
            var lista = new List<Cliente>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync($"Clientes/search?nombre={Uri.EscapeDataString(nombre)}").Result;
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    lista = JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();
                }
            }
            return lista;
        }

        private Cliente? ObtenerClientePorIdDesdeApi(int id)
        {
            Cliente? cli = null;
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.GetAsync($"Clientes/{id}").Result; // GET /api/Clientes/{id}
                if (resp.IsSuccessStatusCode)
                {
                    var json = resp.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<Cliente>(json);
                }
            }
            return cli;
        }

        private void RegistrarClienteEnApi(Cliente c)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var body = new
                {
                    c.Nombre,
                    c.DNI,
                    c.Telefono,
                    c.Direccion,
                    c.Correo
                };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var resp = http.PostAsync("Clientes", content).Result; // POST /api/Clientes
                resp.EnsureSuccessStatusCode();
            }
        }

        private void ActualizarClienteEnApi(Cliente c)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var body = new
                {
                    IdCliente = c.IdCliente,
                    c.Nombre,
                    c.DNI,
                    c.Telefono,
                    c.Direccion,
                    c.Correo,
                    c.Estado
                };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                var resp = http.PutAsync($"Clientes/{c.IdCliente}", content).Result; // PUT /api/Clientes/{id}
                resp.EnsureSuccessStatusCode();
            }
        }

        private bool EliminarClienteEnApi(int id)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var resp = http.DeleteAsync($"Clientes/{id}").Result; // DELETE lógico
                return resp.IsSuccessStatusCode;
            }
        }

        // ================== Acciones MVC ==================

        // GET: /Cliente
        public IActionResult Index(string? nombre, bool soloActivos = false, int page = 1)
        {
            var listado = string.IsNullOrWhiteSpace(nombre)
                ? ObtenerClientesDesdeApi()
                : BuscarClientesPorNombreDesdeApi(nombre);

            if (soloActivos)
                listado = listado.Where(x => x.Estado).ToList();

            // Paginación
            const int pageSize = 10;
            int total = listado.Count;
            int totalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            page = Math.Min(Math.Max(1, page), totalPaginas);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;
            ViewBag.nombre = nombre ?? string.Empty;
            ViewBag.soloActivos = soloActivos;

            var pageItems = listado.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return View(pageItems);
        }

        // GET: /Cliente/Details/5
        public IActionResult Details(int id)
        {
            var cli = ObtenerClientePorIdDesdeApi(id);
            if (cli == null) return NotFound();
            return View(cli);
        }

        // GET: /Cliente/Create
        public IActionResult Create()
        {
            return View(new Cliente { Estado = true }); // por defecto activo
        }

        // POST: /Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente c)
        {
            if (!ModelState.IsValid) return View(c);
            RegistrarClienteEnApi(c);
            return RedirectToAction("Index");
        }

        // GET: /Cliente/Edit/5
        public IActionResult Edit(int id)
        {
            var cli = ObtenerClientePorIdDesdeApi(id);
            if (cli == null) return NotFound();
            return View(cli);
        }

        // POST: /Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente c)
        {
            if (!ModelState.IsValid) return View(c);
            ActualizarClienteEnApi(c);
            return RedirectToAction("Details", new { id = c.IdCliente });
        }

        // GET: /Cliente/Delete/5
        public IActionResult Delete(int id)
        {
            var cli = ObtenerClientePorIdDesdeApi(id);
            if (cli == null) return NotFound();
            return View(cli);
        }

        // POST: /Cliente/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarClienteEnApi(id);
            return RedirectToAction("Index");
        }
    }
}
