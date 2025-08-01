using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models; 

namespace SmileApp.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context; 

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AnalisisYReportes()
        {
            // Pacientes por Género
            var pacientesPorGenero = _context.Pacientes
                .GroupBy(p => p.Sexo)
                .Select(g => new { Sexo = g.Key, Total = g.Count() })
                .ToList();
            ViewBag.PacientesPorGenero = pacientesPorGenero;

            // Citas por Motivo
            var citasPorMotivo = _context.Citas
                .GroupBy(c => c.Motivo)
                .Select(g => new { Motivo = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .ToList();
            ViewBag.CitasPorMotivo = citasPorMotivo;

            // 1. Paciente con más Citas (Top 5)
            // Top 5 Pacientes con más citas usando solo NombrePaciente
            var topPacientes = _context.Citas
                .GroupBy(c => c.NombrePaciente)
                .Select(g => new { Paciente = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .Take(5)
                .ToList();

            ViewBag.TopPacientes = topPacientes;

            // 2. Número de Pacientes por Dirección
            var pacientesPorDireccion = _context.Pacientes
                .GroupBy(p => p.Direccion)
                .Select(g => new { Direccion = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .ToList();
            ViewBag.PacientesPorDireccion = pacientesPorDireccion;

            return View();
        }
    }
}
