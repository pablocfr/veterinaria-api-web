using VeterinariaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VeterinariaApp.Controllers
{
    public class HistorialController : Controller
    {
        private readonly IConfiguration _config;

        public HistorialController(IConfiguration config)
        {
            _config = config;
        }

        #region Métodos auxiliares
        private List<Historial> ObtenerHistoriales()
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var json = http.GetStringAsync("Historiales").Result;
            return JsonConvert.DeserializeObject<List<Historial>>(json) ?? new();
        }

        private Historial? ObtenerPorId(int id)
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var json = http.GetStringAsync($"Historiales/{id}").Result;
            return JsonConvert.DeserializeObject<Historial>(json);
        }

        private List<Historial> BuscarHistoriales(string nombre)
        {
            using var http = new HttpClient { BaseAddress = new Uri(_config["Services:URL"]) };
            var json = http.GetStringAsync($"Historiales/search?nombre={Uri.EscapeDataString(nombre)}").Result;
            return JsonConvert.DeserializeObject<List<Historial>>(json) ?? new();
        }
        #endregion

        #region Acciones
        public IActionResult Index(int page = 1, string? search = null)
        {
            var listado = string.IsNullOrWhiteSpace(search)
                ? ObtenerHistoriales()
                : BuscarHistoriales(search);

            int totalRegistros = listado.Count;
            int registrosPorPagina = 6;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            int omitir = registrosPorPagina * (page - 1);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;
            ViewBag.search = search;

            return View(listado.Skip(omitir).Take(registrosPorPagina));
        }

        public IActionResult Details(int id)
        {
            var historial = ObtenerPorId(id);
            return historial == null ? NotFound() : View(historial);
        }
        #endregion
    }
}