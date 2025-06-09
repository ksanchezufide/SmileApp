using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SmileApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var nombre = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.NombreUsuario = nombre;
            return View();
        }

        public IActionResult GestionPacientes()
        {
            return View();
        }

        public IActionResult GestionPacienteCreate()
        {
            return View();
        }

        public IActionResult CitasMedicas()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View();
        }

        public IActionResult Finanzas()
        {
            return View();
        }

        public IActionResult Reportes()
        {
            return View();
        }

        public IActionResult Seguridad()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
