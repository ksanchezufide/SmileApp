using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmileApp.Models
{
    public class Ingreso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string TipoPago { get; set; }
        public DateTime FechaTransaccion { get; set; }
    }
}