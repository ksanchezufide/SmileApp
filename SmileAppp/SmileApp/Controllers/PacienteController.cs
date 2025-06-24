using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;

namespace SmileApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paciente (modificado para permitir filtros)
        public async Task<IActionResult> Index(string buscar, string sexo)
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            if (!string.IsNullOrEmpty(buscar))
            {
                buscar = buscar.ToLower();
                pacientes = pacientes.Where(p =>
                    (p.Nombre + " " + p.Apellido).ToLower().Contains(buscar)).ToList();
            }

            if (!string.IsNullOrEmpty(sexo))
            {
                sexo = sexo.ToLower();
                pacientes = pacientes.Where(p => p.Sexo.ToLower() == sexo).ToList();
            }

            return View(pacientes);
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Pacientes
                .Include(p => p.Archivos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // GET: Paciente/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente, List<IFormFile> Archivos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();

                await GuardarArchivos(paciente.Id, Archivos);
                return RedirectToAction(nameof(Index));
            }

            return View(paciente);
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente paciente, List<IFormFile> Archivos)
        {
            if (id != paciente.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();

                    await GuardarArchivos(paciente.Id, Archivos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Archivos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente != null)
            {
                // Eliminar archivos físicos
                foreach (var archivo in paciente.Archivos)
                {
                    var rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", archivo.RutaArchivo.TrimStart('/'));
                    if (System.IO.File.Exists(rutaArchivo))
                        System.IO.File.Delete(rutaArchivo);
                }

                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }

        // 🔁 Reutilizable: Método privado para guardar archivos
        private async Task GuardarArchivos(int pacienteId, List<IFormFile> archivos)
        {
            if (archivos == null || archivos.Count == 0)
                return;

            var rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");

            if (!Directory.Exists(rutaBase))
                Directory.CreateDirectory(rutaBase);

            foreach (var archivo in archivos)
            {
                if (archivo.Length > 0)
                {
                    var nombreUnico = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                    var rutaCompleta = Path.Combine(rutaBase, nombreUnico);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                    }

                    var tipo = archivo.ContentType.StartsWith("image") ? "imagen" : "documento";

                    var nuevoArchivo = new ArchivoPaciente
                    {
                        PacienteId = pacienteId,
                        NombreArchivo = archivo.FileName,
                        TipoArchivo = tipo,
                        RutaArchivo = "/archivos/" + nombreUnico,
                    };

                    _context.ArchivosPacientes.Add(nuevoArchivo);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
