using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmileApp.Models
{
    public class Cita
    {
        public int Id { get; set; }

        public DateTime FechaHora { get; set; }

        public string NombrePaciente { get; set; } // ← NUEVO campo en lugar de PacienteId

        public string CorreoPaciente { get; set; }

        public string Motivo { get; set; }

        public string Observaciones { get; set; }

        public string Estado { get; set; } // Ej: "Pendiente", "Confirmada", etc.
    }
}