using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ProductoDto
    {
        public string? identificador { set; get; } = null;

        [Required(ErrorMessage = "Necesario un nombre al producto")]
        public string name { set; get; } = null!;

        [Range(10.00, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal price { set; get; }

        [Range(1, int.MaxValue, ErrorMessage = "Cantidad debe ser mayor a 0")]
        public int amount { set; get; }

        [Range(10.00, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public double kg_standard { set; get; }
    }
}
