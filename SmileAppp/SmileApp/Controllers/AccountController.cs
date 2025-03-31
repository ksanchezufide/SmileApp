using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmileApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password, bool rememberMe)
        {
            // Aquí iría la lógica de autenticación real
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Usuario y contraseña son requeridos");
                return View();
            }

            // Ejemplo básico de validación (en producción usaría Identity)
            if (username == "admin" && password == "admin123")
            {
                // Autenticación exitosa
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Credenciales inválidas");
            return View();
        }
    }
}
