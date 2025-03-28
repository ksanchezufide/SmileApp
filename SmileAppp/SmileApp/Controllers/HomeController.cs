using Microsoft.AspNetCore.Mvc;

namespace SmileApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Acción para la vista de Gestión de Pacientes
        public IActionResult GestionPacientes()
        {
            return View();
        }

        // Acción para la vista de Citas Médicas
        public IActionResult CitasMedicas()
        {
            return View();
        }

        // Acción para la vista de Inventario
        public IActionResult Inventario()
        {
            return View();
        }

        // Acción para la vista de Finanzas
        public IActionResult Finanzas()
        {
            return View();
        }

        // Acción para la vista de Análisis y Reportes
        public IActionResult Reportes()
        {
            return View();
        }

        // Acción para la vista de Seguridad y Acceso
        public IActionResult Seguridad()
        {
            return View();
        }

        // Acción para la vista de Privacidad
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
