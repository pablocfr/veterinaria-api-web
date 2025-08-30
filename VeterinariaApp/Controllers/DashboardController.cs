using Capa_Datos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IConfiguration _config;
        public DashboardController(IConfiguration config)
        {
            _config = config;

        }

        private TotalesDashboard ObtenerTotalesDashboard()
        {
            TotalesDashboard totales = new TotalesDashboard();

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = http.GetAsync("dashboard/totales").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                totales = JsonConvert.DeserializeObject<TotalesDashboard>(data);
            }

            return totales;
        }
            private List<ProximasCitas> ObtenerProximasCitas()
        {
            using var http = new HttpClient();
            http.BaseAddress = new Uri(_config["Services:URL"]);
            var json = http.GetStringAsync("Dashboard/proximas-citas").Result;
            return JsonConvert.DeserializeObject<List<ProximasCitas>>(json) ?? new();
        }
        

        public IActionResult Index(int pagina=1)
        {
            int registrosPorPagina = 3;
            var todasLasCitas = ObtenerProximasCitas();

            int totalRegistros = todasLasCitas.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            int registrosASaltar = (pagina - 1) * registrosPorPagina;

            ViewBag.ProximasCitas = todasLasCitas
                .OrderBy(c => c.FechaHora)
                .Skip(registrosASaltar)
                .Take(registrosPorPagina)
                .ToList();

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            var totales = ObtenerTotalesDashboard();
            return View(totales);
        }
        
    }
}
    
