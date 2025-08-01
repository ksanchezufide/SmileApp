using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmileApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmileApp.Controllers
{
    public class FinanzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinanzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Finanzas/Ingresos
        public async Task<IActionResult> Ingresos()
        {
            var ingresos = await _context.Ingresos
                .OrderByDescending(i => i.FechaTransaccion)
                .ToListAsync();

            ViewBag.TotalIngresos = ingresos.Sum(i => i.Monto);
            ViewBag.TotalEfectivo = ingresos.Where(i => i.TipoPago == "Efectivo").Sum(i => i.Monto);
            ViewBag.TotalTarjeta = ingresos.Where(i => i.TipoPago == "Tarjeta").Sum(i => i.Monto);
            ViewBag.TotalTransferencia = ingresos.Where(i => i.TipoPago == "Transferencia").Sum(i => i.Monto);
            return View(ingresos);
        }

        // GET: /Finanzas/Gastos
        public async Task<IActionResult> Gastos()
        {
            var gastos = await _context.Gastos
                .OrderByDescending(g => g.FechaTransaccion)
                .ToListAsync();

            ViewBag.TotalGastos = gastos.Sum(g => g.Monto);
            return View(gastos);
        }

        // GET: /Finanzas
        public async Task<IActionResult> Index()
        {
            var ingresos = await _context.Ingresos.ToListAsync();
            var gastos = await _context.Gastos.ToListAsync();

            var totalIngresos = ingresos.Sum(i => i.Monto);
            var totalGastos = gastos.Sum(g => g.Monto);

            ViewBag.TotalIngresos = totalIngresos;
            ViewBag.TotalGastos = totalGastos;
            ViewBag.Balance = totalIngresos - totalGastos;

            return View();
        }

        [HttpGet]
        public IActionResult CrearIngreso()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearIngreso(Ingreso ingreso)
        {
            if (ModelState.IsValid)
            {
                ingreso.FechaTransaccion = DateTime.Now;
                _context.Ingresos.Add(ingreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Ingresos));
            }

            return View(ingreso);
        }

        [HttpGet]
        public IActionResult CrearGasto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearGasto(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                gasto.FechaTransaccion = DateTime.Now;
                _context.Gastos.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Gastos));
            }

            return View(gasto);
        }
    }
}
