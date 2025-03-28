using Microsoft.AspNetCore.Mvc;

namespace SmileApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Acci�n para la vista de Gesti�n de Pacientes
        public IActionResult GestionPacientes()
        {
            return View();
        }

        // Acci�n para la vista de Citas M�dicas
        public IActionResult CitasMedicas()
        {
            return View();
        }

        // Acci�n para la vista de Inventario
        public IActionResult Inventario()
        {
            return View();
        }

        // Acci�n para la vista de Finanzas
        public IActionResult Finanzas()
        {
            return View();
        }

        // Acci�n para la vista de An�lisis y Reportes
        public IActionResult Reportes()
        {
            return View();
        }

        // Acci�n para la vista de Seguridad y Acceso
        public IActionResult Seguridad()
        {
            return View();
        }

        // Acci�n para la vista de Privacidad
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
