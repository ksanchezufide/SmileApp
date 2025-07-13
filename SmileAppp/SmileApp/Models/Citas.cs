using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmileApp.Models
{
    public class Cita
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha y hora es obligatoria")]
        [Display(Name = "Fecha y Hora")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio")]
        [MaxLength(255)]
        public string Motivo { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado de la Cita")]
        public EstadoCita Estado { get; set; }

        // Relación con Paciente
        [Required(ErrorMessage = "Debe seleccionar un paciente")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }
    }

    public enum EstadoCita
    {
        Pendiente,
        Confirmada,
        Cancelada,
        Completada
    }
}
