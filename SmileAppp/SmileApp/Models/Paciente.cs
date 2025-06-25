using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SmileApp.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(10)]
        public string Sexo { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(255)]
        public string Direccion { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string Correo { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Relación con archivos
        public ICollection<ArchivoPaciente> Archivos { get; set; }
    }
}

