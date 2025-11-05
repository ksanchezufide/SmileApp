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
            // 1. Pacientes por Género
            var pacientesPorGenero = _context.Pacientes
                .GroupBy(p => p.Sexo)
                .Select(g => new { Sexo = g.Key, Total = g.Count() })
                .ToList();
            ViewBag.PacientesPorGenero = pacientesPorGenero;

            // 2. Citas por Motivo
            var citasPorMotivo = _context.Citas
                .GroupBy(c => c.Motivo)
                .Select(g => new { Motivo = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .ToList();
            ViewBag.CitasPorMotivo = citasPorMotivo;

            // 3. Paciente con más Citas (Top 5)
            // Top 5 Pacientes con más citas usando solo NombrePaciente
            var topPacientes = _context.Citas
                .GroupBy(c => c.NombrePaciente)
                .Select(g => new { Paciente = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .Take(5)
                .ToList();

            ViewBag.TopPacientes = topPacientes;

            // 4. Número de Pacientes por Dirección
            var pacientesPorDireccion = _context.Pacientes
                .GroupBy(p => p.Direccion)
                .Select(g => new { Direccion = g.Key, Total = g.Count() })
                .OrderByDescending(g => g.Total)
                .ToList();
            ViewBag.PacientesPorDireccion = pacientesPorDireccion;

            //NUEVO NUEVO NUEVO 04/11/2025
            // 5. Agrupar Ingresos por mes y año
var ingresosPorMes = _context.Ingresos
    .GroupBy(i => new { i.FechaTransaccion.Year, i.FechaTransaccion.Month })
    .Select(g => new
    {
        Año = g.Key.Year,
        Mes = g.Key.Month,
        TotalIngresos = g.Sum(i => i.Monto)
    })
    .OrderBy(g => g.Año).ThenBy(g => g.Mes)
    .ToList();

// Agrupar Gastos por mes y año
var gastosPorMes = _context.Gastos
    .GroupBy(g => new { g.FechaTransaccion.Year, g.FechaTransaccion.Month })
    .Select(g => new
    {
        Año = g.Key.Year,
        Mes = g.Key.Month,
        TotalGastos = g.Sum(x => x.Monto)
    })
    .OrderBy(g => g.Año).ThenBy(g => g.Mes)
    .ToList();

// Unir ambos resultados por mes y año
var reporteMensual = ingresosPorMes
    .GroupJoin(
        gastosPorMes,
        ingreso => new { ingreso.Año, ingreso.Mes },
        gasto => new { gasto.Año, gasto.Mes },
        (ingreso, gastos) => new
        {
            Año = ingreso.Año,
            Mes = ingreso.Mes,
            TotalIngresos = ingreso.TotalIngresos,
            TotalGastos = gastos.FirstOrDefault()?.TotalGastos ?? 0,
            Balance = ingreso.TotalIngresos - (gastos.FirstOrDefault()?.TotalGastos ?? 0)
        }
    )
    .OrderBy(r => r.Año).ThenBy(r => r.Mes)
    .ToList();

// 6. Citas por Estado 
var citasPorEstado = _context.Citas
    .GroupBy(c => c.Estado)
    .Select(g => new { Estado = g.Key, Total = g.Count() })
    .OrderByDescending(g => g.Total)
    .ToList();
ViewBag.CitasPorEstado = citasPorEstado;

            return View();
        }
    }
}

