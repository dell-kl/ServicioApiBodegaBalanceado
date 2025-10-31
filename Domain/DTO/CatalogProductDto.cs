using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class CatalogProductDto
    {
        public string identificador { set; get; } = "";

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string nombreProducto { set; get; } = null!;

        public ICollection<DataProduct> dataCatalogProducts { set; get; } = new List<DataProduct>();

    }

    public class DataProduct
    {
        [Required(ErrorMessage = "Debes ingresar un precio de venta")]
        [Range(5, double.MaxValue, ErrorMessage = "Debes ingresar un precio mayor o igual a $5")]
        public decimal precio { set; get; }

        [Required(ErrorMessage = "Debes ingresar un peso para que este vinculado con el precio")]
        [Range(20, double.MaxValue, ErrorMessage = "Debes ingresar un peso mayor o igual a 20 KG")]
        public double pesoKg { set; get; }

        public int cantidadTotal { set; get; }
    }
}