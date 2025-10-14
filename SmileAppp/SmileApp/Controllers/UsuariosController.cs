using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmileApp.Models;

namespace SmileApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filtro)
        {
            return RedirectToAction("Seguridad", "Home", new { filtro });
        }

        public IActionResult Create()
        {
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            ModelState.Remove("Rol");

            if (ModelState.IsValid)
            {
                usuario.FechaRegistro = DateTime.Now;
                usuario.Estado = true;

                _context.Entry(usuario).Property(u => u.FechaRegistro).CurrentValue = DateTime.Now;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Seguridad", "Home");
            }

            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            ModelState.Remove("Rol");

            if (id != usuario.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioExistente = await _context.Usuarios
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);

                    if (usuarioExistente == null)
                        return NotFound();

                    usuario.FechaRegistro = usuarioExistente.FechaRegistro;
                    usuario.Estado = usuarioExistente.Estado;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuarios.Any(u => u.Id == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction("Seguridad", "Home");
            }


            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.RolId);
            return View(usuario);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Seguridad", "Home");
        }
    }
}