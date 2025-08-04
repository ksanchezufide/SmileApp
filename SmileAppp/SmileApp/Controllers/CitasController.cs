using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;
using SmileApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmileApp.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var citas = await _context.Citas
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();

            return View(citas);
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id);
            if (cita == null)
                return NotFound();

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();

                // Enviar correo al paciente
                var emailService = new EmailService();
                await emailService.EnviarConfirmacionCitaAsync(
                    cita.CorreoPaciente,
                    cita.NombrePaciente,
                    cita.FechaHora
                );

                return RedirectToAction(nameof(Index));
            }

            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return NotFound();

            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cita cita)
        {
            if (id != cita.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id);
            if (cita == null)
                return NotFound();

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}