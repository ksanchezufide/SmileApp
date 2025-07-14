using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;
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
            var citas = _context.Citas.Include(c => c.Paciente);
            return View(await citas.ToListAsync());
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "Id",
                "NombreCompleto"
            );

            ViewData["Estado"] = new SelectList(Enum.GetValues(typeof(EstadoCita)));

            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHora,Motivo,Observaciones,Estado,PacienteId")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar los dropdowns en caso de error para que la vista los tenga disponibles
            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "Id",
                "NombreCompleto",
                cita.PacienteId
            );

            ViewData["Estado"] = new SelectList(Enum.GetValues(typeof(EstadoCita)), cita.Estado);

            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "Id",
                "NombreCompleto",
                cita.PacienteId
            );

            ViewData["Estado"] = new SelectList(Enum.GetValues(typeof(EstadoCita)), cita.Estado);

            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHora,Motivo,Observaciones,Estado,PacienteId")] Cita cita)
        {
            if (id != cita.Id) return NotFound();

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

            ViewData["PacienteId"] = new SelectList(
                _context.Pacientes.Select(p => new {
                    p.Id,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }),
                "Id",
                "NombreCompleto",
                cita.PacienteId
            );

            ViewData["Estado"] = new SelectList(Enum.GetValues(typeof(EstadoCita)), cita.Estado);

            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
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
