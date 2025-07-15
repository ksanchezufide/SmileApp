using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SmileApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SmileApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var nombre = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.NombreUsuario = nombre;
            return View();
        }

        public IActionResult Seguridad(string filtro)
        {
            if (!UsuarioTieneRol("Administrador"))
                return RedirectToAction("AccesoDenegado");

            var usuarios = _context.Usuarios
                                   .Include(u => u.Rol)
                                   .AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                usuarios = usuarios.Where(u => u.Nombre.Contains(filtro) || u.Correo.Contains(filtro));
            }

            return View(usuarios.ToList());
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

        public IActionResult CitasMedicas()
        {
            return View();
        }
    }
}
