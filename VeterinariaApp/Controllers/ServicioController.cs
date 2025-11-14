using VeterinariaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace VeterinariaApp.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IConfiguration _config;

        public ServicioController(IConfiguration config)
        {
            _config = config;
        }

        #region Métodos auxiliares
        private List<Servicio> ObtenerServicios()
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var json = http.GetStringAsync("Servicios").Result;
            return JsonConvert.DeserializeObject<List<Servicio>>(json) ?? new();
        }

        private Servicio? ObtenerPorId(int id)
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var json = http.GetStringAsync($"Servicios/{id}").Result;
            return JsonConvert.DeserializeObject<Servicio>(json);
        }

        private bool RegistrarServicio(Servicio s)
        {
            var dto = new { s.Nombre, s.Descripcion, s.Precio };
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            return http.PostAsync("Servicios", content).Result.IsSuccessStatusCode;
        }

        private bool ActualizarServicio(Servicio s)
        {
            var dto = new { s.Nombre, s.Descripcion, s.Precio };
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            return http.PutAsync($"Servicios/{s.IdServicio}", content).Result.IsSuccessStatusCode;
        }

        private bool EliminarServicio(int id)
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            return http.DeleteAsync($"Servicios/{id}").Result.IsSuccessStatusCode;
        }
        #endregion

        #region Acciones
        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerServicios();
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
            var servicio = ObtenerPorId(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Servicio servicio)
        {
            if (!ModelState.IsValid) return View(servicio);
            if (RegistrarServicio(servicio))
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Error al crear el servicio.");
            return View(servicio);
        }

        public IActionResult Edit(int id)
        {
            var servicio = ObtenerPorId(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        [HttpPost]
        public IActionResult Edit(Servicio servicio)
        {
            if (!ModelState.IsValid) return View(servicio);
            if (ActualizarServicio(servicio))
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Error al actualizar el servicio.");
            return View(servicio);
        }

        public IActionResult Delete(int id)
        {
            var servicio = ObtenerPorId(id);
            return servicio == null ? NotFound() : View(servicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarServicio(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}