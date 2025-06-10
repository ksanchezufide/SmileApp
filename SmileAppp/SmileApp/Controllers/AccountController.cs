using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SmileApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ModelState.Remove(nameof(model.MensajeError));

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("Errores del modelo:");
                foreach (var error in errores)
                {
                    Console.WriteLine(error);
                }

                return View(model);
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == model.Correo && u.Estado == true);

            if (usuario == null)
            {
                model.MensajeError = "No se encontró un usuario con ese correo o está inactivo.";
                return View(model);
            }

            if (usuario.ContraseñaHash != model.Contraseña)
            {
                model.MensajeError = "Contraseña incorrecta.";
                return View(model);
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
            HttpContext.Session.SetString("Rol", usuario.Rol?.Nombre ?? "");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(
            string firstName,
            string lastName,
            string email,
            string username,
            string password,
            string confirmPassword,
            string role,
            bool acceptTerms)
        {
            TempData["RegistrationMessage"] = "Registro simulado correctamente";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
