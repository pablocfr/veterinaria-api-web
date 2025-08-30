using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using VeterinariaApp.Models;

namespace VeterinariaApp.Controllers
{
    public class VentasController : Controller
    {
        private readonly IConfiguration _config;
        public VentasController(IConfiguration config)
        {
            _config = config;
        }

        #region
        private List<Detalle_Venta> ObtenerVenta()
        {
            var listado = new List<Detalle_Venta>();

            using (var ventaHTTP = new HttpClient())
            {

                ventaHTTP.BaseAddress = new Uri(_config["Services:URL"]);
                var mensaje = ventaHTTP.GetAsync("Ventas").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                //Convertir los datos de tipo string(Json en objeto)
                listado = JsonConvert.DeserializeObject<List<Detalle_Venta>>(data);
            }
            return listado;
        }
        private Venta RegistrarDetalleVenta(Venta venta, List<Detalle_Venta> detalles)
        {
            Venta nuevaVenta = null;

            using (var ventaHTTP = new HttpClient())
            {
                ventaHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                // Construir el DTO que espera el API
                var ventaDto = new
                {
                    IdCliente = venta.IdCliente,
                    Total = venta.Total,
                    Detalles = detalles.Select(d => new
                    {
                        d.IdProducto,
                        d.IdServicio,
                        d.Cantidad,
                        d.SubTotal
                    }).ToList()
                };

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(ventaDto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var mensaje = ventaHTTP.PostAsync("Ventas", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                nuevaVenta = JsonConvert.DeserializeObject<Venta>(data);
            }

            return nuevaVenta;
        }
        private List<Cliente> ObtenerClientes()
        {
            var listado = new List<Cliente>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var mensaje = http.GetAsync("Clientes").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                listado = JsonConvert.DeserializeObject<List<Cliente>>(data);
            }
            return listado ?? new List<Cliente>();
        }

        private List<Producto> ObtenerProductos()
        {
            var listado = new List<Producto>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var mensaje = http.GetAsync("Producto").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                listado = JsonConvert.DeserializeObject<List<Producto>>(data);
            }
            return listado ?? new List<Producto>();
        }
        private List<Servicio> ObtenerServicios()
        {
            var listado = new List<Servicio>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var mensaje = http.GetAsync("Servicios").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                listado = JsonConvert.DeserializeObject<List<Servicio>>(data);
            }
            return listado ?? new List<Servicio>();
        }
        #endregion
        public IActionResult Index()
        {
            var listado = ObtenerVenta();
            return View(listado);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = new SelectList(ObtenerClientes(), "IdCliente", "Nombre");
            ViewBag.Productos = new SelectList(ObtenerProductos(), "IdProducto", "Nombre");
            ViewBag.Servicios = new SelectList(ObtenerServicios(), "IdServicio", "Nombre");


            return View(new Venta());

        }
        [HttpPost]
        public IActionResult Create(Venta venta, List<Detalle_Venta> detalles)
        {
            RegistrarDetalleVenta(venta, detalles);
            return RedirectToAction("Index"); ;
        }


    }
}
