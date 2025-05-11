
using System.ComponentModel.DataAnnotations;


namespace SmileApp.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad en stock.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Debe ingresar el precio del producto.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; } = true;

        [Required(ErrorMessage = "Debe ingresar el stock mínimo.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
        public int StockMinimo { get; set; } = 5; // Se puede ajustar por producto

        // Propiedad calculada (no se guarda en DB)
        public bool BajoStock => Cantidad <= StockMinimo && Cantidad > 0;

        public bool Agotado => Cantidad == 0;
    }
}