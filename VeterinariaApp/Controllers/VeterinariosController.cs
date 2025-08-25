using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class VeterinariosController : Controller
    {
        private readonly IConfiguration _config;
        public VeterinariosController(IConfiguration config)
        {
            _config = config;
        }

        #region
        private List<Veterinario> ObtenerVeterinario()
        {
            var listado = new List<Veterinario>();
            //Declaramos un veterinario HTTP
            using (var veterinarioHTTP = new HttpClient())
            {
                //definimos la direccion/url base
                veterinarioHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                //Obtenenemos el mensaje de respuesta

                var mensaje = veterinarioHTTP.GetAsync("Veterinario").Result;

                //Leemos el contenido
                var data = mensaje.Content.ReadAsStringAsync().Result;

                //Convertir los datos de tipo string(Json en objeto)
                listado = JsonConvert.DeserializeObject<List<Veterinario>>(data);
            }
            return listado;
        }

        private Veterinario registrarVeterinario(Veterinario veterinario)
        {
            Veterinario nuevoVeterinario = null;
            using (var veterinarioHTTP = new HttpClient())
            {
                veterinarioHTTP.BaseAddress = new Uri(_config["Services:URL"]);
                StringContent contenido = new StringContent(JsonConvert.SerializeObject(veterinario),
                    System.Text.Encoding.UTF8, "application/json");

                var mensaje = veterinarioHTTP.PostAsync("Veterinario", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                nuevoVeterinario = JsonConvert.DeserializeObject<Veterinario>(data);

            }

            return nuevoVeterinario;
        }

        private Veterinario obtenerPorId(int id)
        {
            Veterinario veterinario = null;
            using (var veterinarioHTTP = new HttpClient())
            {
                veterinarioHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = veterinarioHTTP.GetAsync($"veterinario/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                veterinario = JsonConvert.DeserializeObject<Veterinario>(data);
            }
            return veterinario;
        }

        private Veterinario actualizarVeterinario(Veterinario veterinario)
        {
            using (var veterinarioHTTP = new HttpClient())
            {
                veterinarioHTTP.BaseAddress = new Uri(_config["Services:URL"]);
                var contenido = new StringContent(JsonConvert.SerializeObject(veterinario),
                    System.Text.Encoding.UTF8, "application/json");

                var mensaje = veterinarioHTTP.PutAsync($"veterinario/{veterinario.IDVeterinario}", contenido).Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                veterinario = JsonConvert.DeserializeObject<Veterinario>(data);
            }

            return veterinario;
        }

        private Veterinario eliminarVeterinario(int id)
        {
            Veterinario veterinario = null;
            using (var veterinarioHTTP = new HttpClient())
            {
                veterinarioHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = veterinarioHTTP.DeleteAsync($"veterinario/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                // Si el endpoint devuelve el objeto eliminado
                veterinario = JsonConvert.DeserializeObject<Veterinario>(data);
            }
            return veterinario;
        }

        #endregion
        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerVeterinario();
            int totalRegistros = listado.Count;
            int registrosPorPaginas = 4;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPaginas);
            int omitir = registrosPorPaginas * (page - 1);
            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;
            return View(listado.Skip(omitir).Take(registrosPorPaginas));
        }

        public IActionResult Create()
        {
            return View(new Veterinario());
        }

        [HttpPost]
        public IActionResult Create(Veterinario veterinario)
        {
            registrarVeterinario(veterinario);

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var veterinario = obtenerPorId(id);
            return View(veterinario);
        }

        [HttpPost]
        public IActionResult Edit(Veterinario veterinario)
        {
            actualizarVeterinario(veterinario);
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var veterinario = obtenerPorId(id);
            return View(veterinario);
        }

        [HttpDelete]
        public IActionResult Delete()

        {
            return RedirectToAction("index");
        }
    }

}

