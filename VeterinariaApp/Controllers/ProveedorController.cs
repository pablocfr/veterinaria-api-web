using VeterinariaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeterinariaApp.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly IConfiguration _config;

        public ProveedorController(IConfiguration config)
        {
            _config = config;
        }

        // Métodos privados para consumir la API
        private List<Proveedor> ObtenerProveedores()
        {
            var listado = new List<Proveedor>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var response = http.GetAsync("Proveedores").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Proveedor>>(data);
                }
            }
            return listado;
        }

        private Proveedor ObtenerPorId(int id)
        {
            Proveedor proveedor = null;
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var response = http.GetAsync($"Proveedores/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    proveedor = JsonConvert.DeserializeObject<Proveedor>(data);
                }
            }
            return proveedor;
        }

        private Proveedor RegistrarProveedor(Proveedor proveedor)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(
                    JsonConvert.SerializeObject(proveedor),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = http.PostAsync("Proveedores", contenido).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Proveedor>(data);
                }
            }
            return null;
        }

        private bool ActualizarProveedor(Proveedor proveedor)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(
                    JsonConvert.SerializeObject(proveedor),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = http.PutAsync($"Proveedores/{proveedor.IdProveedor}", contenido).Result;
                return response.IsSuccessStatusCode;
            }
        }

        private bool EliminarProveedor(int id)
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var response = http.DeleteAsync($"Proveedores/{id}").Result;
                return response.IsSuccessStatusCode;
            }
        }

        // Acciones para las vistas
        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerProveedores();
            int totalRegistros = listado.Count;
            int registrosPorPagina = 6;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            int omitir = registrosPorPagina * (page - 1);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;

            return View(listado.Skip(omitir).Take(registrosPorPagina));
        }

        public IActionResult Details(int id)
        {
            var proveedor = ObtenerPorId(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Proveedor proveedor)
        {
            var nuevoProveedor = new
            {
                Nombre = proveedor.Nombre,
                Ruc = proveedor.Ruc,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion,
                Correo = proveedor.Correo
            };

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(
                    JsonConvert.SerializeObject(nuevoProveedor),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = http.PostAsync("Proveedores", contenido).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(proveedor);
        }

        public IActionResult Edit(int id)
        {
            var proveedor = ObtenerPorId(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult Edit(Proveedor proveedor)
        {
            var proveedorActualizado = new
            {
                Nombre = proveedor.Nombre,
                Ruc = proveedor.Ruc,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion,
                Correo = proveedor.Correo
            };

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(
                    JsonConvert.SerializeObject(proveedorActualizado),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = http.PutAsync($"Proveedores/{proveedor.IdProveedor}", contenido).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(proveedor);
        }

        public IActionResult Delete(int id)
        {
            var proveedor = ObtenerPorId(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (EliminarProveedor(id))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Delete", new { id });
        }
    }
}