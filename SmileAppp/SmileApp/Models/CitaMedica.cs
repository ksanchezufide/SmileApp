using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmileApp.Models
{
    public class CitaMedica
    {
        public int Id { get; set; }

        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente Paciente { get; set; }

        [Required]
        [Display(Name = "Fecha de la Cita")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Hora")]
        public TimeSpan Hora { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Confirmada, Pendiente, Cancelada
    }
}

