using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;

namespace SmileApp.Controllers
{
    public class CitaMedicasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitaMedicasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CitaMedica
        public async Task<IActionResult> Index()
        {
            var citas = await _context.CitasMedicas
                .Include(c => c.Paciente)  // Incluir datos del paciente
                .OrderByDescending(c => c.Fecha)  // Ordenar por fecha descendente
                .ToListAsync();

            // Contadores para el resumen
            ViewBag.Confirmadas = citas.Count(c => c.Estado == "Confirmada");
            ViewBag.Pendientes = citas.Count(c => c.Estado == "Pendiente");
            ViewBag.Canceladas = citas.Count(c => c.Estado == "Cancelada");

            return View(citas);
        }

        // GET: CitaMedica/Create
        public IActionResult Create()
        {
            // Obtener lista de pacientes para el dropdown
            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .Select(p => new {
                    p.Id,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre} - {p.Cedula}"
                }), "Id", "NombreCompleto");

            return View();
        }

        // POST: CitaMedica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
       
        public async Task<IActionResult> Create(CitaMedica cita)
        {
            Console.WriteLine($"ModelState isValid: {ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cita);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cita creada exitosamente!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al crear cita: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar la cita.");
                }
            }
            else
            {
                // Mostrar errores de validación
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }

            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .Select(p => new {
                    p.Id,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre} - {p.Cedula}"
                }), "Id", "NombreCompleto");

            return View(cita);
        }

        // GET: CitaMedica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitasMedicas
                .Include(c => c.Paciente)  // Incluir datos del paciente
                .FirstOrDefaultAsync(m => m.Id == id);

            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // GET: CitaMedica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitasMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            // Obtener lista de pacientes para el dropdown
            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .Select(p => new {
                    p.Id,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre} - {p.Cedula}"
                }), "Id", "NombreCompleto", citaMedica.PacienteId);

            return View(citaMedica);
        }

        // POST: CitaMedica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,Fecha,Hora,Estado")] CitaMedica cita)
        {
            if (id != cita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar que no exista una cita para el mismo paciente a la misma hora y fecha
                    var citaExistente = await _context.CitasMedicas
                        .FirstOrDefaultAsync(c => c.PacienteId == cita.PacienteId &&
                                                 c.Fecha == cita.Fecha &&
                                                 c.Hora == cita.Hora &&
                                                 c.Id != cita.Id);

                    if (citaExistente != null)
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una cita para este paciente en la misma fecha y hora.");
                    }
                    else
                    {
                        _context.Update(cita);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Cita actualizada exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaMedicaExists(cita.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Si hay errores, recargar la lista de pacientes
            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .OrderBy(p => p.Apellido)
                .ThenBy(p => p.Nombre)
                .Select(p => new {
                    p.Id,
                    NombreCompleto = $"{p.Apellido}, {p.Nombre} - {p.Cedula}"
                }), "Id", "NombreCompleto", cita.PacienteId);

            return View(cita);
        }

        // GET: CitaMedica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.CitasMedicas
                .Include(c => c.Paciente)  // Incluir datos del paciente
                .FirstOrDefaultAsync(m => m.Id == id);

            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // POST: CitaMedica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citaMedica = await _context.CitasMedicas.FindAsync(id);
            _context.CitasMedicas.Remove(citaMedica);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cita eliminada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        // POST: CitaMedica/Notificar/5
        [HttpPost]
        public async Task<IActionResult> Notificar(int id)
        {
            var cita = await _context.CitasMedicas
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                return NotFound();
            }

            // Aquí iría la lógica para enviar la notificación por correo
            // Por ahora solo simulamos el envío

            TempData["SuccessMessage"] = $"Notificación enviada al paciente {cita.Paciente.Nombre} {cita.Paciente.Apellido}";
            return RedirectToAction(nameof(Index));
        }

        private bool CitaMedicaExists(int id)
        {
            return _context.CitasMedicas.Any(e => e.Id == id);
        }
    }
}