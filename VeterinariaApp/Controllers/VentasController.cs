using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using TuProyectoWeb.Filtros;
using VeterinariaApp.DTOs;
using VeterinariaApp.Models;
using VeterinariaApp.Models.ViewModelVenta;

namespace VeterinariaApp.Controllers
{
    [FiltroSesion]
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
        private string RegistrarDetalleVenta(VentasRegistrarDTO dto)
        {
            using (var ventaHTTP = new HttpClient())
            {
                ventaHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var mensaje = ventaHTTP.PostAsync("Ventas", contenido).Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;

                // Retornar mensaje del API
                var respuesta = JsonConvert.DeserializeObject<dynamic>(data);
                return respuesta.Mensaje;
            }
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

        private Detalle_Venta ObtenerDetalleVentaPorId(int id)
        {
            {
                Detalle_Venta detalle = null;

                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(_config["Services:URL"]);

                    var mensaje = http.GetAsync($"Ventas/busqueda/{id}").Result;

                    if (mensaje.IsSuccessStatusCode)
                    {
                        var data = mensaje.Content.ReadAsStringAsync().Result;
                        detalle = JsonConvert.DeserializeObject<Detalle_Venta>(data);
                    }
                }

                return detalle;
            }
        }
        private List<Detalle_Venta> ObtenerDetalleVentaPorIdVenta(int id)
        {
            var listado = new List<Detalle_Venta>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(_config["Services:URL"]);
                var mensaje = http.GetAsync($"Ventas/busqueda/cabecera/detalle/{id}").Result;
                var data = mensaje.Content.ReadAsStringAsync().Result;
                listado = JsonConvert.DeserializeObject<List<Detalle_Venta>>(data);
            }
            return listado ?? new List<Detalle_Venta>();
        }
        private Cabecera_Venta ObtenerCabeceraVentaPorId(int id)
        {
            {
                Cabecera_Venta cabecera = null;

                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(_config["Services:URL"]);

                    var mensaje = http.GetAsync($"Ventas/busqueda/cabecera/{id}").Result;

                    if (mensaje.IsSuccessStatusCode)
                    {
                        var data = mensaje.Content.ReadAsStringAsync().Result;
                        cabecera = JsonConvert.DeserializeObject<Cabecera_Venta>(data);
                    }
                }

                return cabecera;
            }
        }
        #endregion
        public IActionResult Index(int page = 1)
        {
            var listado = ObtenerVenta();
            int totalRegistros = listado.Count;
            int registrosPorPaginas = 6;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPaginas);

            // Calcular los registros a omitir
            int omitir = registrosPorPaginas * (page - 1);

            // Enviar datos a la vista
            ViewBag.totalPaginas = totalPaginas;
            ViewBag.paginaActual = page;

            return View(listado.Skip(omitir).Take(registrosPorPaginas).ToList());
        }

        public IActionResult Create()
        {
            // SelectList para dropdowns en la vista
            ViewBag.Clientes = new SelectList(ObtenerClientes(), "IdCliente", "Nombre");
            ViewBag.Productos = new SelectList(ObtenerProductos(), "IdProducto", "Nombre");
            ViewBag.Servicios = new SelectList(ObtenerServicios(), "IdServicio", "Nombre");

            // Retornar un DTO vacío que la vista pueda usar
            return View(new VentasRegistrarDTO());
        }

        [HttpPost]
        public IActionResult Create(VentasRegistrarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                // Recargar dropdowns en caso de error
                ViewBag.Clientes = new SelectList(ObtenerClientes(), "IdCliente", "Nombre");
                ViewBag.Productos = new SelectList(ObtenerProductos(), "IdProducto", "Nombre");
                ViewBag.Servicios = new SelectList(ObtenerServicios(), "IdServicio", "Nombre");
                return View(dto);
            }



            // Llamada a la API
            var mensaje = RegistrarDetalleVenta(dto);

            // Mostrar Mensaje
            TempData["Mensaje"] = mensaje;

            // Redireccionar al índice
            return RedirectToAction("Create");
        }

        public IActionResult Details(int id)
        {
            var detalle = ObtenerDetalleVentaPorId(id);

            return View(detalle);
        }

        public IActionResult ImprimirVenta()
        {
            Detalle_Venta ultimaVenta = ObtenerVenta().OrderByDescending(v => v.IdVenta).FirstOrDefault();

            int idVenta = ultimaVenta.IdVenta;

            Cabecera_Venta cabecera = ObtenerCabeceraVentaPorId(idVenta);
            List<Detalle_Venta> detalles = ObtenerDetalleVentaPorIdVenta(idVenta);

            if (cabecera == null)
            {
                return NotFound("No se encontró la cabecera de la venta.");
            }

            // Construir el ViewModelVenta con los datos obtenidos
            ViewModelVenta modelo = new ViewModelVenta
            {
                numeroventa = cabecera.idVenta.ToString(),
                documentocliente = cabecera.dni,
                nombrecliente = cabecera.nombreCliente,
                subtotal = detalles.Sum(d => d.SubTotal).ToString("F2"),
                impuesto = "0.00", // si no manejas impuestos, puedes dejarlo fijo o calcularlo
                total = cabecera.total.ToString("F2"),
                detalleventa = detalles.Select(d => new ViewModelDetalleVenta
                {
                    producto = d.Producto != null ? d.Producto.Nombre : d.Servicio?.Nombre,
                    cantidad = d.Cantidad.ToString(),
                    precio = d.Producto != null ? d.Producto.Precio.ToString("F2") : d.Servicio?.Precio.ToString("F2"),
                    total = d.SubTotal.ToString("F2")
                }).ToList()
            };

            // Retornar PDF
            return new ViewAsPdf("ImprimirVenta", modelo)
            {
                FileName = $"Venta_{modelo.numeroventa}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

    }
}
