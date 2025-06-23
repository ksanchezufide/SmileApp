using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmileApp.Models



{
    public class ArchivoPaciente
    {
        public int Id { get; set; }

        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NombreArchivo { get; set; }

        [MaxLength(50)]
        public string TipoArchivo { get; set; }

        [MaxLength(500)]
        public string RutaArchivo { get; set; }

        public Paciente Paciente { get; set; }
    }
}
