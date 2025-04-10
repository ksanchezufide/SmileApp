using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmileApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login (mantenido igual como referencia)
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

        // GET: /Account/Register (solo visual)
        [HttpGet]
        public IActionResult Register()
        {
            // Vista solo para visualización, sin lógica
            return View();
        }

        // POST: /Account/Register (simulado)
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
            // Solo redirecciona al login con mensaje simulado
            TempData["RegistrationMessage"] = "Registro simulado correctamente";
            return RedirectToAction("Login");
        }

        // GET: /Account/AccessDenied (opcional)
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}