using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _config;

        public ProductoController(IConfiguration config)
        {
            _config = config;
        }

        #region Métodos Privados

        private List<Producto> ObtenerProductos()
        {
            var listado = new List<Producto>();

            using (var productoHTTP = new HttpClient())
            {
                productoHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = productoHTTP.GetAsync("Producto").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                listado = JsonConvert.DeserializeObject<List<Producto>>(data);
            }

            return listado;
        }

        private Producto RegistrarProducto(Producto producto)
        {
            Producto nuevoProducto = null;

            using (var productoHTTP = new HttpClient())
            {
                productoHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var contenido = new StringContent(JsonConvert.SerializeObject(producto),
                    System.Text.Encoding.UTF8, "application/json");

                var mensaje = productoHTTP.PostAsync("Producto", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                nuevoProducto = JsonConvert.DeserializeObject<Producto>(data);
            }

            return nuevoProducto;
        }
        private Producto ObtenerPorId(int id)
        {
            Producto producto = null;

            using (var productoHTTP = new HttpClient())
            {
                productoHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = productoHTTP.GetAsync($"Producto/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                producto = JsonConvert.DeserializeObject<Producto>(data);
            }

            return producto;
        }
        private Producto ActualizarProducto(Producto producto)
        {
            using (var productoHTTP = new HttpClient())
            {
                productoHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var contenido = new StringContent(JsonConvert.SerializeObject(producto),
                    System.Text.Encoding.UTF8, "application/json");

                var mensaje = productoHTTP.PutAsync($"Producto/{producto.IdProducto}", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                producto = JsonConvert.DeserializeObject<Producto>(data);
            }

            return producto;
        }
        private Producto EliminarProducto(int id)
        {
            Producto producto = null;

            using (var productoHTTP = new HttpClient())
            {
                productoHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = productoHTTP.DeleteAsync($"Producto/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                producto = JsonConvert.DeserializeObject<Producto>(data);
            }

            return producto;
        }
        #endregion
        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerProductos();
            int totalRegistros = listado.Count;
            int registrosPorPagina = 4;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
            int omitir = registrosPorPagina * (page - 1);

            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;

            return View(listado.Skip(omitir).Take(registrosPorPagina));
        }
        public IActionResult Create()
        {
            return View(new Producto());
        }
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            RegistrarProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var producto = ObtenerPorId(id);
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            ActualizarProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var producto = ObtenerPorId(id);
            return View(producto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            EliminarProducto(id);
            return RedirectToAction("Index");
        }

    }
}