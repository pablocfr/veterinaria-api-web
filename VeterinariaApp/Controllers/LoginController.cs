using Microsoft.AspNetCore.Mvc;
using Capa_Negocio;
using Microsoft.AspNetCore.Http;

namespace ProyEcommerceVentas.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string usuario, string clave)
        {
            var user = usuarioNegocio.ValidarLogin(usuario, clave);
            if (user != null)
            {
                HttpContext.Session.SetString("usuario", user.Nombre);
                HttpContext.Session.SetString("rol", user.Rol);

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.Mensaje = "Usuario o clave incorrectos";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
