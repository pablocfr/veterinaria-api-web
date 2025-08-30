using VeterinariaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace VeterinariaApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly IConfiguration _config;

        public CitaController(IConfiguration config)
        {
            _config = config;
        }

        private List<Cita> ObtenerCitas()
        {
            var listado = new List<Cita>();

            using (var citaHTTP = new HttpClient())
            {
                citaHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = citaHTTP.GetAsync("Citas").Result;

                    var data = mensaje.Content.ReadAsStringAsync().Result;
                //convierte los datos tipo string(json) en objeto
                    listado = JsonConvert.DeserializeObject<List<Cita>>(data);
                
            }
            return listado;
        }
        private Cita registrarCita(Cita cita)
        {
            Cita nuevaCita = null;

            using (var citaHTTP = new HttpClient())
            {
                citaHTTP.BaseAddress = new Uri(_config["Services:URL"]);
                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(cita),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var mensaje = citaHTTP.PostAsync("Citas", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                nuevaCita = JsonConvert.DeserializeObject<Cita>(data);
            }
            return nuevaCita;
        }
        private List<Mascota> ObtenerMascotas()
        {
            var listado = new List<Mascota>();

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var response = http.GetAsync("Mascotas").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Mascota>>(data);
                }
            }
            return listado;
        }
        private List<Veterinario> ObtenerVeterinarios()
        {
            var listado = new List<Veterinario>();

            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var response = http.GetAsync("Veterinario").Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    listado = JsonConvert.DeserializeObject<List<Veterinario>>(data);
                }
            }
            return listado;
        }

        private Cita ObtenerPorId(int id)
        {
            Cita cita = null;

            using (var citaHTTP = new HttpClient())
            {
                citaHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = citaHTTP.GetAsync($"Citas/busqueda/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                cita = JsonConvert.DeserializeObject<Cita>(data);
            }
            return cita;
        }
        private Cita actualizarCita(Cita cita)
        {
            using (var citaHTTP = new HttpClient())
            {
                citaHTTP.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(JsonConvert.SerializeObject(cita),
                    System.Text.Encoding.UTF8, "application/json");

                var mensaje = citaHTTP.PutAsync($"Citas", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                cita = JsonConvert.DeserializeObject<Cita>(data);
            }
            return cita;
        }
        private bool eliminarCita(int id)
        {
            using (var citaHTTP = new HttpClient())
            {
                citaHTTP.BaseAddress = new Uri(_config["Services:URL"]);

var mensaje = citaHTTP.DeleteAsync($"Citas?id={id}").Result;
                return mensaje.IsSuccessStatusCode;
            }
        }

        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerCitas();
            int totalRegistros = listado.Count;
            int registrosPorPagina = 6;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            int omitir = registrosPorPagina * (page - 1);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;

            return View(listado.Skip(omitir).Take(registrosPorPagina));
        }
        public IActionResult Calendario(DateTime? fecha)
        {
            var listado = ObtenerCitas();

            DateTime fechaSeleccionada = fecha ?? DateTime.Today;

            var citasFiltradas = listado
                .Where(c => c.Fecha.Date == fechaSeleccionada.Date)
                .OrderBy(c => c.Fecha)
                .ToList();

            ViewBag.FechaSeleccionada = fechaSeleccionada;

            return View(citasFiltradas);
        }
        public IActionResult Create()
        {
            var mascotas = ObtenerMascotas();
            var veterinarios = ObtenerVeterinarios();

            var motivos = new List<string>
    {
                 "Vacunación",
                 "Control de crecimiento",
                 "Desparasitación",
                 "Chequeo general",
                 "Emergencia"
    };

            ViewBag.Mascotas = new SelectList(mascotas, "IdMascota", "Nombre");
            ViewBag.Veterinarios = new SelectList(veterinarios, "IDVeterinario", "NombreCompleto");
            ViewBag.Motivos = new SelectList(motivos);

            return View(new Cita());
        }

        [HttpPost]
        public IActionResult Create(Cita cita)
        {
            registrarCita(cita);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var cita = ObtenerPorId(id);
            if (cita == null) return NotFound();

            // Listas para los <select>
            ViewBag.Mascotas = new SelectList(ObtenerMascotas(), "IdMascota", "Nombre", cita.IdMascota);
            ViewBag.Veterinarios = new SelectList(ObtenerVeterinarios(), "IDVeterinario", "NombreCompleto", cita.IDVeterinario);

            var motivos = new List<string>
    {
        "Vacunación","Control de crecimiento","Desparasitación","Chequeo general","Emergencia"
    };
            ViewBag.Motivos = new SelectList(motivos, cita.Motivo);

            // ❗ Formato para datetime-local (ISO 8601 sin segundos)
            ViewBag.FechaFormateada = cita.Fecha.ToString("yyyy-MM-ddTHH:mm");

            return View(cita);
        }

        [HttpPost]
        public IActionResult Edit(Cita cita)
        {
            actualizarCita(cita);
            return RedirectToAction("Details", new {id = cita.IdCita});
        }

        public IActionResult Details(int id)
        {
            var cita = ObtenerPorId(id);

            return View(cita);
        }
        public IActionResult Delete(int id)
        {
            var cita = ObtenerPorId(id);
            if (cita == null) return NotFound();
            return View(cita);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            eliminarCita(id);
            return RedirectToAction("Index");
        }
    }

    }

