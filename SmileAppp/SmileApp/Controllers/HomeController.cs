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
            if (!UsuarioTieneRol("Administrador", "Dentista"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult GestionPacienteCreate()
        {
            if (!UsuarioTieneRol("Administrador", "Dentista"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult CitasMedicas()
        {
            if (!UsuarioTieneRol("Administrador", "Dentista"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult Inventario()
        {
            if (!UsuarioTieneRol("Administrador"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult Finanzas()
        {
            if (!UsuarioTieneRol("Administrador"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult Reportes()
        {
            if (!UsuarioTieneRol("Administrador"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult Seguridad()
        {
            if (!UsuarioTieneRol("Administrador"))
                return RedirectToAction("AccesoDenegado");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        private bool UsuarioTieneRol(params string[] rolesPermitidos)
        {
            var rolUsuario = HttpContext.Session.GetString("Rol");
            return rolesPermitidos.Contains(rolUsuario);
        }
    }
}
