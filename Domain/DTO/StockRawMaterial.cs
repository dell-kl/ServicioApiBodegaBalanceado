using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class StockRawMaterial
    {
        [Required(ErrorMessage = "Debes ingresar el identificador")]
        public string Identificador { set; get; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Debes ingresar por lo menos un valor de 1")]
        public int Amount { set; get; }

        [Range(5, double.MaxValue, ErrorMessage = "Debes ingresar un valor mayor a 5KG")]
        public double kgStandard { set; get; }

        [Range(2, double.MaxValue, ErrorMessage = "Debes ingresar un valor mayor a 2 dolares")]
        public double PriceUnit { set; get; }
    }
}
