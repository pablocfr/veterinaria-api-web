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
        

        public IActionResult Index(int page=1)
        {
            const int pageSize = 3;
            var allCitas = ObtenerProximasCitas();

            int totalRecords = allCitas.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            int skip = (page - 1) * pageSize;

            ViewBag.ProximasCitas = allCitas
                .OrderBy(c => c.FechaHora)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            var totales = ObtenerTotalesDashboard();
            return View(totales);
        }
        
    }
}
    
